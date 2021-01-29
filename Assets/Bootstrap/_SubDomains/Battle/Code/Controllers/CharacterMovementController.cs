using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Bootstrap._SubDomains.Battle.Code.Controllers
{
    public class CharacterMovementController : MonoBehaviour
    {
        [Inject] private BattleManager _battleManager;
        
        private int _currentPathIndex = -1;
        private List<(int, int)> _currentPath;
        private bool _isMoving = false;
        
        public async UniTask MoveToPath(List<(int, int)> path)
        {
            if(_isMoving)
                return;

            _isMoving = true;

            transform.DOPath(path.CoordinatesToVector(), 1f).OnComplete(() =>
            {
                _battleManager.OnCharacterFinishedMovement();
                _isMoving = false;
            });

            while (_isMoving)
                await UniTask.DelayFrame(1);
        }
    }
}