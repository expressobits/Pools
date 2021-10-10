using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace ExpressoBits.Pools.Tests
{
    public class PoolTest
    {

        // A Test gameobject is correct instantiante prefab
        [Test]
        public void InstantiateCorrectGameObjectTest()
        {
            Pool pool = ScriptableObject.CreateInstance<Pool>();
            GameObject prefab = new GameObject();
            pool.Setup(new PoolSettings(){ IncreaseSize = 1 },prefab);
            GameObject obj = pool.Instantiate();
            Assert.AreNotEqual(obj,prefab);
        }

        [Test]
        [System.Obsolete]
        public void InstantiateCorrectPositionAndRotationTest()
        {
            Pool pool = ScriptableObject.CreateInstance<Pool>();
            GameObject prefab = new GameObject();
            pool.Setup(new PoolSettings(){ IncreaseSize = 1 },prefab);
            Vector3 position = Vector3.up;
            Quaternion rotation = Quaternion.EulerAngles(60,0f,0f);
            GameObject obj = pool.Instantiate(position,rotation);
            Assert.AreEqual(obj.transform.position,position);
            Assert.AreEqual(obj.transform.rotation,rotation);
        }

        [Test]
        public void DestroyObjectInPoolCountTest()
        {
            Pool pool = ScriptableObject.CreateInstance<Pool>();
            GameObject prefab = new GameObject();
            pool.Setup(new PoolSettings(){ IncreaseSize = 1 },prefab);
            GameObject obj = pool.Instantiate();
            pool.Destroy(obj);
            Assert.AreEqual(pool.Objects.Count,1);
        }

        [Test]
        public void ClearPoolCountTest()
        {
            Pool pool = ScriptableObject.CreateInstance<Pool>();
            GameObject prefab = new GameObject();
            pool.Setup(new PoolSettings(){ IncreaseSize = 5 },prefab);
            GameObject obj = pool.Instantiate();
            pool.Clear();
            Assert.AreEqual(pool.Objects.Count,0);
        }

        // // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // // `yield return null;` to skip a frame.
        // [UnityTest]
        // public IEnumerator PoolTest.pools_Tests_Runtime_NewTestScriptWithEnumeratorPasses()
        // {
        //     // Use the Assert class to test conditions.
        //     // Use yield to skip a frame.
        //     yield return null;
        // }
    }
}
