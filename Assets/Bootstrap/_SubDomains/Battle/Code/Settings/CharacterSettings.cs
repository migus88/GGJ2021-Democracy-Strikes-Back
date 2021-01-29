using UnityEngine;

namespace Bootstrap._SubDomains.Battle.Code.Settings
{
    [CreateAssetMenu(fileName = "CharacterSettings", menuName = "Atomic/Character Settings", order = 0)]
    public class CharacterSettings : ScriptableObject
    {
        public int MaxActionPoints = 4;
        public float TileMovementAnimationSpeed = 0.3f;
    }
}