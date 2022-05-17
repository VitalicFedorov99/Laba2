using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDistance : MonoBehaviour
{
    public  GameObject obj1;
    public  GameObject obj2;

   
    private void Update() 
    {

    if(Input.GetKeyDown(KeyCode.A))
    {
        Debug.Log(Vector3.Distance(obj1.transform.position,obj2.transform.position));
    }    
    }

}
