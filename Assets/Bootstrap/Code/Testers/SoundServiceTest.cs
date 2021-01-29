using Bootstrap.Code.Services;
using Bootstrap.Code.Settings;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Bootstrap.Code.Testers
{
    public class SoundServiceTest : MonoBehaviour
    {
        [Inject] private SoundService _soundService;

        [Button]
        public async void TestAudioRetrieval()
        {
            _soundService.PlaySound(SoundSettings.SoundType.Click);
        }
    }
}