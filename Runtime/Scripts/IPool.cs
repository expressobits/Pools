using UnityEngine;

namespace ExpressoBits.Pools
{
    public interface IPool
    {
        GameObject Prefab { get; }
        GameObject Instantiate();
        GameObject Instantiate(Vector3 position,Quaternion rotation);
        void Destroy(GameObject gameObject);
        void Setup(PoolSettings settings,GameObject prefab);
        void Enqueue(GameObject obj);
        GameObject Dequeue(Vector3 position, Quaternion rotation);
    }
}

