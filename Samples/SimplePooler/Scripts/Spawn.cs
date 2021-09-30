using UnityEngine;

public class Spawn : MonoBehaviour
{

    public bool isEnablePool;
    public float fireTime = 0.1f;
    public int count = 10;
    public float rangeRandomSize;

    public GameObject prefab;
    
    private void Fire()
    {
        for (int i = 0; i < count; i++)
        {
            if(isEnablePool){
                this.InstantiateInPool(prefab,
                    GetSpawnPosition(gameObject.transform.position),
                    Quaternion.identity);
            }else{
                Instantiate(prefab,
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