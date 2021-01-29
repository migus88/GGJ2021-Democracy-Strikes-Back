using Bootstrap.Code.Services;
using Bootstrap.Code.Settings;
using UnityEngine;
using Zenject;

namespace Bootstrap._SubDomains.MainMenu.Code
{
    public class MenuScenesManager : MonoBehaviour
    {
        [SerializeField] private SceneSettings _sceneSettings;
        [Inject] private SceneService _sceneService;

        public async void StartGame()
        {
            await _sceneService.LoadScenes(_sceneSettings);
        }
    }
}