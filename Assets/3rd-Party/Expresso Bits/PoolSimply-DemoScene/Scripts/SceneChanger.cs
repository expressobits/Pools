using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public void ChangeScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex == 0?1:0);
    }
}
