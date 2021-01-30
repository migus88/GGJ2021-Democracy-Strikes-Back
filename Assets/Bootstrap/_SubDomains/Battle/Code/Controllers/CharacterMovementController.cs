using System;
using System.Collections.Generic;
using System.Linq;
using Atomic.Pathfinding.Core.Helpers;
using Bootstrap._SubDomains.Battle.Code.Data;
using Bootstrap._SubDomains.Battle.Code.Settings;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Bootstrap._SubDomains.Battle.Code.Controllers
{
    public class CharacterMovementController : MonoBehaviour
    {
        public CharacterSettings Settings { get; set; }

        [SerializeField] private CharacterAnimationController _animationController;

        [Inject] private BattleManager _battleManager;
        [Inject] private Field _field;
        [Inject(Id = "All")] private Dictionary<string, Character> _allCharacters;

        private bool _isMoving = false;
        private Character _character;

        private void Start()
        {
            _animationController.Stand();
        }

        public void Init(Character character)
        {
            _character = character;
        }

        public async UniTask MoveToPath(List<PathCell> path, float speedMultiplier = 1f)
        {
            if (_isMoving)
                return;

            _isMoving = true;

            transform.DOPath(path.CoordinatesToVector(),
                    (Settings.TileMovementAnimationSpeed * speedMultiplier) * path.Count)
                .SetEase(Ease.Linear)
                .OnWaypointChange(i =>
                {
                    if (i + 1 >= path.Count)
                    {
                        _animationController.Stand();
                        return;
                    }
                    
                    _animationController.Walk();

                    var location = path[i + 1];
                    _animationController.Turn(location.Direction.ToAnimDirection());

                    if(!_field.Matrix[location.Coordinates.Y(), location.Coordinates.X()].IsOccupied)
                        return;

                    var character = _allCharacters
                        .FirstOrDefault(c => c.Value != _character && c.Value.Origin == location.Coordinates).Value;
                    character.MoveOneTile(location.Direction);
                })
                .OnComplete(() =>
                {
                    _animationController.Stand();
                    _isMoving = false;
                    _battleManager.OnCharacterFinishedMovement(_character);

                    var tile = _field.GetTile(_character.Origin);

                    if (!tile)
                    {
                        _animationController.Fall();
                        _character.Die();
                    }
                });

            while (_isMoving)
                await UniTask.DelayFrame(1);
        }

        public void Die()
        {
            transform.DOMoveY(transform.position.y - 18, Settings.FallDuration).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }
}