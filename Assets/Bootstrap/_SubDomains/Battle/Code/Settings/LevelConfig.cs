using UnityEngine;

namespace Bootstrap._SubDomains.Battle.Code.Settings
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Atomic/Level Config", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        public LevelPlayerConfig[] Players;

        [System.Serializable]
        public class LevelPlayerConfig
        {
            public bool IsBot = false;
            public CharacterSettings[] Characters;
        }
    }
}