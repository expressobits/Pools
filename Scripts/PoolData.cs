using UnityEngine;

namespace ExpressoBits.Pools
{
    [CreateAssetMenu(fileName = "PoolData", menuName = "Pool/PoolData", order = 0)]
    public class PoolData : ScriptableObject
    {
        [SerializeField]
        [Range(8, 255)]
        [Tooltip("Amount of a batch of gameObjects after there are no items in the pool")]
        private byte increaseAmount = 32;

        [SerializeField] [Range(8, 255)] [Tooltip("Amount instantiated and saved as soon as the pool is started")]
        private byte initialIncrease = 32;

        // [SerializeField] [Tooltip("Color tag use to window pools")]
        // public bool sendEventMessageToIpoolers = false;

        [Header("Editor configurations")] [SerializeField] [Tooltip("Color tag use to window pools")]
        private Color color = Color.black;


        public byte InitialIncrease => initialIncrease;
        public Color Color => color;
    }
}