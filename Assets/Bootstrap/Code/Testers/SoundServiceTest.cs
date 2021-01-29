using Bootstrap.Code.Services;
using Bootstrap.Code.Settings;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Bootstrap.Code.Testers
{
    public class SoundServiceTest : MonoBehaviour
    {
        [SerializeField] private SoundService _soundService;
        [SerializeField] private SoundSettings _soundSettings;


        [Button]
        public async void TestAudioRetrieval()
        { _soundService.RetrieveAudioClip(SoundSettings.SoundType.BACKGROUND,_soundSettings);
        }
    }
}