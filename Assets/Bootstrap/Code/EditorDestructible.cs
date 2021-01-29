using UnityEngine;

namespace Bootstrap.Code
{
   
    public class EditorDestructible : MonoBehaviour
    {
        private void Awake()
        {
            #if UNITY_EDITOR
            Destroy(gameObject);
            #endif
        }
    } 
}