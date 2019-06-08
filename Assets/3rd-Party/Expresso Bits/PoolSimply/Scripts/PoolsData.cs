using UnityEngine;

namespace ExpressoBits.PoolSimply
{
    [CreateAssetMenu(fileName = "PoolsData", menuName = "ExpressoBits/PoolSimply/PoolsData", order = 0)]
    public class PoolsData : ScriptableObject 
    {

        public bool onlyStableConfigurations = true;
        public int initialAmount = 20;
        public int increaseAmount = 5;

        [Header("Unstable configurations")]
        public bool willGrow = true;
        public int maxAmountObjects = 50;

    }
}
