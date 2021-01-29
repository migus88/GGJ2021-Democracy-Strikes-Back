using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Bootstrap.Code.Settings;
using Cysharp.Threading.Tasks;
using ModestTree;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Bootstrap.Code.Services
{
    public class SoundService : MonoBehaviour
    {
        [SerializeField] private SoundSettings _soundSettings;

        public void PlaySound(SoundSettings.SoundType type)
        {
            AudioClip clip = null;
            if(_soundSettings.AudioList.TryGetValue(type,out clip))
            {
                Debug.Log($"Got the sound clip");
            }
        }
    }
}