using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionJump : AbstractState
{
    
    [SerializeField] private float _speed;

    [SerializeField] private GameObject _objJump;

    private Rigidbody _rb;
    

    public void Jump(GameObject obj)
    {
        Vector3 jumpPos = obj.transform.position - transform.position;
        //transform.position=Vector3.MoveTowards(transform.position, new Vector3(jumpPos.x,jumpPos.y+2,jumpPos.z),_speed*Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, obj.transform.position, _speed * Time.deltaTime);
    }

    public void Jump()
    {
        //_rb.isKinematic = false;
        
        Debug.Log("JUMp");
        _rb.AddForce(Vector3.up * _speed + (-Vector3.forward * _speed));
        
    }
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _context.GetComponent<Context>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Platform1Movement platform))
        {
            gameObject.transform.SetParent(platform.transform);

            //_rb.isKinematic = true;
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.TryGetComponent(out Platform1Movement platform))
        {
            Debug.Log("ZamenaState");
            gameObject.transform.parent=null;
            GetComponent<Context>().SetupState(MinionState.Move);
           // _context.SetupState(MinionState.Move);
            //_rb.isKinematic = true;
        }
    }


    private void Update()
    {
        // if(_objJump!=null)
        // {
        //     Jump(_objJump);
        // }    
        if (Input.GetKeyDown(KeyCode.G))
        {
            Jump();
        }
    }





}
