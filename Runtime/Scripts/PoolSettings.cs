using UnityEngine;

namespace ExpressoBits.Pools
{
    [System.Serializable]
    public struct PoolSettings
    {
        [Tooltip("Amount of a batch of gameObjects after there are no items in the pool")]
        [SerializeField,Min(1)] public uint IncreaseSize;
    }
}