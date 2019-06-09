using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExpressoBits.PoolSimply;

public class Destroy : MonoBehaviour {

    public bool isEnablePool;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isEnablePool){
            this.DestroyInPool(collision.gameObject);
        }else{
            Destroy(collision.gameObject);
        }
        
    }
    
}
