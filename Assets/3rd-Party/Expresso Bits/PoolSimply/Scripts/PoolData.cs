using UnityEngine;

namespace ExpressoBits.PoolSimply
{
    [CreateAssetMenu(fileName = "PoolData", menuName = "ExpressoBits/PoolSimply/PoolData", order = 0)]
    public class PoolData : ScriptableObject 
    {

        [SerializeField] [Range(8,256)] [Tooltip("Amount of a batch of gameObjects after there are no items in the pool")]
        public short increaseAmount = 32;
        [SerializeField] [Range(8,256)] [Tooltip("Amount instantiated and saved as soon as the pool is started")]
        public short initialIncrease = 32;

        // [Header("Unstable configurations")]
        // public bool willGrow = true;
        // public int maxAmountObjects = 50;

    }
}
