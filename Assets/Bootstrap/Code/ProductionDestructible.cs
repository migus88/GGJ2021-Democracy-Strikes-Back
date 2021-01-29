using UnityEngine;

namespace Bootstrap.Code
{
   
    public class ProductionDestructible : MonoBehaviour
    {
        private void Awake()
        {
            #if UNITY_EDITOR
            Destroy(gameObject);
            #endif
        }
    } 
}