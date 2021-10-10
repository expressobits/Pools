using UnityEngine;

namespace ExpressoBits.Pools
{
    public interface IPool
    {
        GameObject Prefab { get; }

        /// <summary>
        /// Instantiate an object that can be in the pool, if there are no objects in the pool yet,
        /// it will create objects based on the amount configured in the pool settings
        /// This version of the method configures the position and rotation of the object once activated
        /// </summary>
        /// <returns>GameObject instantiated</returns>
        GameObject Instantiate();

        /// <summary>
        /// Instantiate an object that can be in the pool, if there are no objects in the pool yet,
        /// it will create objects based on the amount configured in the pool settings
        /// </summary>
        /// <param name="position">GameObject Position</param>
        /// <param name="rotation">GameObject Rotation</param>
        /// <returns>GameObject instantiated</returns>
        GameObject Instantiate(Vector3 position,Quaternion rotation);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        void Destroy(GameObject gameObject);

        /// <summary>
        /// Configure this pool with pool Settings and its respective prefab
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="prefab"></param>
        void Setup(PoolSettings settings,GameObject prefab);

        /// <summary>
        /// Clears the deactivated objects in the pool
        /// </summary>
        void Clear();

        /// <summary>
        /// Instantiates a number of disabled gameobjects in the pool
        /// </summary>
        /// <param name="count">Number of gameobject</param>
        void InstantiateInPoolAmount(int count);
    }
}

