using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExpressoBits.PoolSimply;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour,IPooler {
    
    private Rigidbody2D rb;

    public void OnPoolerDisable()
    {
        //Example use interface IPooler
    }

    public void OnPoolerEnable()
    {
        //Example use interface IPooler
        rb.velocity = new Vector3(0f, 0f, 0f);
        rb.angularVelocity = 0f;
    }

    void Awake () {
        rb = GetComponent<Rigidbody2D>();
    }
}
