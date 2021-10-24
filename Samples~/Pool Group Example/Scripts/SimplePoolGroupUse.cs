using System.Collections;
using System.Collections.Generic;
using ExpressoBits.Pools;
using UnityEngine;

public class SimplePoolGroupUse : MonoBehaviour
{

    public PoolGroup poolGroup; 

    public IEnumerator Start()
    {
        
        for (int i = 0; i < 50; i++)
        {
            GameObject obj2 = poolGroup.InstantiateARandom(new Vector3(Random.Range(-5f,5f),Random.Range(-5f,5f),0f),Quaternion.identity);
            yield return new WaitForSeconds(0.4f);
            this.DestroyInPool(obj2,Random.Range(1f, 5f));
        }
    }
    
}
