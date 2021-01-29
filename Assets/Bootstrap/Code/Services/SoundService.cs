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
        public void RetrieveAudioClip(SoundSettings.SoundType type,SoundSettings settings)
        {
            AudioClip clip = null;
            if(settings.AudioList.TryGetValue(type,out clip))
            {
                Debug.Log($"Got the sound clip");
            }
        }
    }
}