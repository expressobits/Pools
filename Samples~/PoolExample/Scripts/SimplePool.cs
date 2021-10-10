using System.Collections;
using System.Collections.Generic;
using ExpressoBits.Pools;
using UnityEngine;

public class SimplePool : MonoBehaviour
{

    public Pool pool; 

    public IEnumerator Start()
    {
        
        for (int i = 0; i < 50; i++)
        {
            GameObject obj = pool.Instantiate(new Vector3(Random.Range(-5f,5f),Random.Range(-5f,5f),0f),Quaternion.identity);
            GameObject obj2 = this.InstantiateInPool(pool.Prefab,new Vector3(Random.Range(-5f,5f),Random.Range(-5f,5f),0f),Quaternion.identity);
            yield return new WaitForSeconds(0.4f);
            StartCoroutine(DelayToDestroy (obj,Random.Range(1f, 5f)));
            StartCoroutine(DelayToDestroy (obj2,Random.Range(1f, 5f)));
        }
    }

    private IEnumerator DelayToDestroy(GameObject obj,float seconds)
    {
        yield return new WaitForSeconds(seconds);
        pool.Destroy(obj);
    }
    
}
