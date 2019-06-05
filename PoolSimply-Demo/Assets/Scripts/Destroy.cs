using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.DestroyInPool(collision.gameObject);
    }
    
}
