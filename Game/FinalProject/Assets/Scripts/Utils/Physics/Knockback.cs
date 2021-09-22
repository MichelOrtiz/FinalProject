using UnityEngine;
namespace FinalProject.Assets.Scripts.Utils.Physics
{
    [System.Serializable]
    public class Knockback
    {
        public float duration;
        public float force;

        [Range(0, 360)]
        public float angle;
    }
}