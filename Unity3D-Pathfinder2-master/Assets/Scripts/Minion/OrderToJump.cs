using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderToJump : MonoBehaviour
{
    [SerializeField] private MinionJump _jumpMinion;

    [SerializeField] private float _dist=7f;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(Vector3.Distance(gameObject.transform.position,_jumpMinion.transform.position));
        if(other.gameObject.TryGetComponent(out Platform1Movement platform)&&Vector3.Distance(gameObject.transform.position,_jumpMinion.transform.position)<_dist)
        {
            _jumpMinion.Jump();
        }
    }

}
