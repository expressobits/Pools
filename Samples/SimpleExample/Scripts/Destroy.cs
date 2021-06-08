using UnityEngine;

public class Destroy : MonoBehaviour {

    public bool isEnablePool;

    private void OnTriggerEnter(Collider collider)
    {
        if(isEnablePool){
            this.DestroyInPool(collider.gameObject);
        }else{
            Destroy(collider.gameObject);
        }
        
    }
    
}