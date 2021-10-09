using System.Collections.Generic;
using UnityEngine;

namespace ExpressoBits.Pools
{
    [CreateAssetMenu(menuName = "ExpressoBits/Pool", fileName = "Pool")]
    public class Pool : ScriptableObject
    {
        public PoolData Data => data;
        [SerializeField] private PoolData data;

        public GameObject Instantiate(Vector3 position, Quaternion rotation)
        {
            return PoolManager.Instantiate(data, position, rotation);
        }

        public GameObject Instantiate()
        {
            return PoolManager.Instantiate(data);
        }

        public void Destroy(GameObject gameobject)
        {
            PoolManager.Destroy(gameobject, data);
        }
    }
}
