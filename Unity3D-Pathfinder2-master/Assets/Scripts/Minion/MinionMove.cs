using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMove : AbstractState
{


    [SerializeField] private float _speed;

    [SerializeField] private LocalPathFinder _pathFinder;



    [SerializeField] private GameObject _pos;

    [SerializeField] private List<GameObject> _listPath = new List<GameObject>();

    [SerializeField] private float _dist = 7f;

    [SerializeField] private int countPos = 0;

    [SerializeField] private bool _moveStop = false;

    private Rigidbody _rb;

    public void Move()
    {

        transform.position = Vector3.MoveTowards(transform.position, _pos.transform.position, _speed * Time.fixedDeltaTime);
        if (Vector3.Distance(gameObject.transform.position, _pos.transform.position) < 0.5f&& countPos < _listPath.Count)
        {
            countPos++;
            _pos = _listPath[countPos];
        }
        if (Vector3.Distance(gameObject.transform.position, _pos.transform.position) > _dist)
        {
            Debug.Log("Distance:" + (Vector3.Distance(gameObject.transform.position, _pos.transform.position)));
            _context.SetupState(MinionState.Jump);
        }
        if (countPos >= _listPath.Count)
        {   
            Debug.Log("End"+_listPath.Count );
            _pos=null;
            GetComponent<Context>().SetupState(MinionState.Rotate);
        }
        //_rb.MovePosition(_pos.transform.position*Time.deltaTime*_speed);
    }

    public void SetList()
    {
        _listPath = _pathFinder.GetFullPath();
        _pos = _listPath[countPos];
        _context.SetupState(MinionState.Move);
    }

    public void SetPos(GameObject pos)
    {
        _pos = pos;
    }

    private void FixedUpdate()
    {
        if (_pos != null && MinionState.Move == _context.GetState())
        {
            Move();
        }
    }
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _context = GetComponent<Context>();
    }
}
