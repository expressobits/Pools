using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExpressoBits.PoolSimply;

public class Destroy : MonoBehaviour {

    public Pool pool;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.DestroyInPool(collision.gameObject,pool);
    }
    
}
