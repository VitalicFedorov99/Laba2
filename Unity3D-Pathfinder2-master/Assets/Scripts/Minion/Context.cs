using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Context : MonoBehaviour
{
    [SerializeField] private MinionState _state;
    
    public void SetupState(MinionState state)
    {   
        Debug.Log("StateSetup");
        _state = state;
    }


    public MinionState GetState()
    {
        return _state;
    }

    


}
