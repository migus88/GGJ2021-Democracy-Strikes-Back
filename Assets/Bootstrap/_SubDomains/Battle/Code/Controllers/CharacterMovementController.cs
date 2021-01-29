using System.Collections.Generic;
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
        
        private int _currentPathIndex = -1;
        private List<(int, int)> _currentPath;
        private bool _isMoving = false;
        
        public async UniTask MoveToPath(List<PathCell> path)
        {
            if(_isMoving)
                return;

            _isMoving = true;

            transform.DOPath(path.CoordinatesToVector(), Settings.TileMovementAnimationSpeed * path.Count).OnComplete(() =>
            {
                _battleManager.OnCharacterFinishedMovement();
                _isMoving = false;
            });

            while (_isMoving)
                await UniTask.DelayFrame(1);
        }
    }
}