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
        public void TestAudioRetrieval()
        {
            _soundService.PlaySound(SoundSettings.SoundType.Click);
        }
        
        [Button]
        public void TestPlayMusic()
        {
            _soundService.PlayMusic(SoundSettings.MusicType.Menu);
        }
    }
}