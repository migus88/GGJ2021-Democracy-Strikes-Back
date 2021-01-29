using System;
using System.Collections.Generic;
using Bootstrap.Code.Helpers;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Bootstrap.Code.Settings
{
    [CreateAssetMenu(fileName = "Sound", menuName = "Atomic/Settings/Sound", order = 0)]
    public class SoundSettings : ScriptableObject
    {
        public SceneAudioList AudioList;
        public SceneMusicList MusicList;
        public enum SoundType
        {
          Click = 0,
          Wind = 1,
          Fart = 2
        }

        public enum MusicType
        {
            Menu = 0,
            Intro = 1,
            Battle = 2,
            Credits = 3
        }
        
        [Serializable]
        public class SceneAudioList : UnitySerializedDictionary<SoundType, AudioClip> { }
        [Serializable]
        public class SceneMusicList : UnitySerializedDictionary<MusicType, AudioClip> { }
    }
}