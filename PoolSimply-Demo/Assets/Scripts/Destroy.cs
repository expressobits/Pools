using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

    public bool isPoolSimplyEnable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isPoolSimplyEnable){
            this.DestroyInPool(collision.gameObject);
        }else{
            Destroy(collision.gameObject);
        }
        
    }
    
}
