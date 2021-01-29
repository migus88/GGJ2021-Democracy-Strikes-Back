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

        [Inject] private BattleManager _battleManager;
        [Inject] private Field _field;
        [Inject(Id = "All")] private Dictionary<string, Character> _allCharacters;

        private bool _isMoving = false;
        private Character _character;
        //private Queue<(List<PathCell>, float)> _queue = new Queue<(List<PathCell>, float)>();

        public void Init(Character character)
        {
            _character = character;
        }

        public async UniTask MoveToPath(List<PathCell> path, float speedMultiplier = 1f)
        {
            if (_isMoving)
            {
                //_queue.Enqueue((path, speedMultiplier));
                return;
            }

            _isMoving = true;

            transform.DOPath(path.CoordinatesToVector(),
                    (Settings.TileMovementAnimationSpeed * speedMultiplier) * path.Count)
                .SetEase(Ease.Linear)
                .OnWaypointChange(i =>
                {
                    if (i + 1 >= path.Count)
                        return;

                    var location = path[i + 1];

                    if(!_field.Matrix[location.Coordinates.Y(), location.Coordinates.X()].IsOccupied)
                        return;

                    var character = _allCharacters
                        .FirstOrDefault(c => c.Value != _character && c.Value.Origin == location.Coordinates).Value;
                    character.MoveOneTile(location.Direction);
                })
                .OnComplete(() =>
                {
                    _isMoving = false;
                    _battleManager.OnCharacterFinishedMovement(_character);

                    var tile = _field.GetTile(_character.Origin);

                    if (!tile)
                    {
                        _character.Die();
                    }
                });

            while (_isMoving)
                await UniTask.DelayFrame(1);

            /*if (_queue.Count > 0)
            {
                var newTarget = _queue.Dequeue();
                await MoveToPath(newTarget.Item1, newTarget.Item2);
            }*/
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