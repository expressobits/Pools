using UnityEngine;

namespace ExpressoBits.PoolSimply
{
    [CreateAssetMenu(fileName = "PoolData", menuName = "ExpressoBits/PoolSimply/PoolData", order = 0)]
    public class PoolData : ScriptableObject 
    {

        [SerializeField] [Range(5,50)] [Tooltip("Amount of a batch of gameObjects after there are no items in the pool")]
        public int increaseAmount = 25;

        // [Header("Unstable configurations")]
        // public int initialAmount = 20;
        // public bool willGrow = true;
        // public int maxAmountObjects = 50;

    }
}
