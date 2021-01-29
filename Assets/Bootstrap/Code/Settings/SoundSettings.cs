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
        public enum SoundType
        {
          CLICK,
          WIND,
          FART,
          BACKGROUND
        }

        [Serializable]
        public class SceneAudioList : UnitySerializedDictionary<SoundType, AudioClip> { }
    }
}