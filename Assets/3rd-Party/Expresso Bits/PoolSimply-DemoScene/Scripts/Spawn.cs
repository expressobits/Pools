using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExpressoBits.PoolSimply;
using UnityEngine.Events;

public class Spawn : MonoBehaviour
{

    public bool isEnablePool;
    public float fireTime = 0.1f;
    public int count = 10;
    public float rangeRandomSize;

    public GameObject ballPrefab;
    public GameObject stonePrefab;

    private void Fire()
    {
        for (int i = 0; i < count; i++)
        {
            if(isEnablePool){
                this.InstantiateInPool(ballPrefab,
                    GetSpawnPosition(gameObject.transform.position),
                    Quaternion.identity);

                this.InstantiateInPool(stonePrefab,
                    GetSpawnPosition(gameObject.transform.position),
                    Quaternion.identity);
            }else{
                Instantiate(ballPrefab,
                    GetSpawnPosition(gameObject.transform.position),
                    Quaternion.identity);

                Instantiate(stonePrefab,
                    GetSpawnPosition(gameObject.transform.position),
                    Quaternion.identity);
            }
            
        }
    }

    private Vector3 GetSpawnPosition(Vector3 vector3)
    {
        return new Vector3(vector3.x + Random.Range(-rangeRandomSize, rangeRandomSize), vector3.y, vector3.z);
    }

    //Enable spawn fire balls
    private void OnEnable()
    {
        InvokeRepeating("Fire", fireTime, fireTime);
    }

    //Disable spawn fire balls
    private void OnDisable()
    {
        CancelInvoke("Fire");
    }
}
