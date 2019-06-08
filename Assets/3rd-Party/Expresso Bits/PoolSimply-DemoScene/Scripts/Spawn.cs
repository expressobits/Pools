using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExpressoBits.PoolSimply;

public class Spawn : MonoBehaviour
{
    public float fireTime = 0.1f;
    public int count = 10;
    public GameObject ballPrefab;
    public float rangeRandomSize;

    public Pools pools;

    public bool isPoolSimplyEnable;

    private void Fire()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject bullet;
            if(isPoolSimplyEnable){
                bullet = this.InstantiateInPool(ballPrefab,pools);
            }else{
                bullet = Instantiate(ballPrefab);
            }
            bullet.transform.position = GetSpawnPosition(gameObject.transform.position);
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
