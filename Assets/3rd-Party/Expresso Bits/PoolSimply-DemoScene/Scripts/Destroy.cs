using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExpressoBits.PoolSimply;

public class Destroy : MonoBehaviour {

    public bool isPoolSimplyEnable;

    public Pools pools;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isPoolSimplyEnable){
            this.DestroyInPool(collision.gameObject,pools);
        }else{
            Destroy(collision.gameObject);
        }
        
    }
    
}
