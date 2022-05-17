using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionRotate : AbstractState
{
    [SerializeField] private MinionState _state;

    [SerializeField] private GameObject _objRotate;

    public void Rotate()
    {

    
       /* if (_objRotate.transform.position.x < transform.position.x)
        {
            transform.Rotate(0f, -30f, 0f, Space.World);
            //transform.rotation=Quaternion.LookRotation(_objRotate.transform.position-transform.position);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, _objRotate.transform.rotation, 2*Time.deltaTime);

        }
        if(_objRotate.transform.position.x>transform.position.x)
        {
            transform.Rotate(0f, 30f, 0f, Space.World);
        }
        //if(_objRotate.transform.position.z<transform.position.z)
        */
    }

    private void Update()
    {   
        if(Input.GetKeyDown(KeyCode.A))
        {
            Rotate();
        }
    }


}
