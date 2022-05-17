using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalPathFinder : MonoBehaviour
{
    [SerializeField] private GlobalRegion[] _regions;
    [SerializeField] private GameObject _startPoint;

    [SerializeField] private GameObject _endPoint;

    [SerializeField] private GlobalRegion _regionStartPoint;
    [SerializeField] private GlobalRegion _regionEndPoint;

    [SerializeField] private List<GlobalRegion> _path = new List<GlobalRegion>();




    [SerializeField] private LocalPathFinder _localPathFinder;
    [SerializeField] private int _idRegion = 0;
    public int IDRegion { get { return _idRegion; } }

    public void Setup()
    {
        SetIsDesiredRegions(_startPoint, _endPoint);
        ImportanceRegion();
        CreatePath();
    }

    public GlobalRegion[] GetGlobalRegion()
    {
        return _regions;
    }
    public void SetIsDesiredRegions(GameObject startPoint, GameObject endPoint)
    {
        bool isStartPoint = false, isEndPoint = false;
        foreach (GlobalRegion reg in _regions)
        {
            isStartPoint = reg.CheckDotInRegion(startPoint.transform.position);
            isEndPoint = reg.CheckDotInRegion(endPoint.transform.position);
            AppointRegion(isStartPoint, isEndPoint, reg);
        }

    }

    public void UpdateLocalPathFinder()
    {
        if (_idRegion < _regions.Length - 1)
        {
            Debug.Log("Update" + _idRegion);
            _idRegion++;
            if (_path[_idRegion] ==_regionEndPoint)
            {
                Debug.Log("Конец пути"+ _path[_idRegion].name +" ID"+_idRegion);
                _localPathFinder.Setup(_path[_idRegion], _endPoint);
            }
            else
            {
                SetupGlobalRegionInLocalPathFinder(IDRegion);

            }
            if (_path[_idRegion].IsJumpRegion == true && _path[_idRegion + 1].IsJumpRegion == true)
            {
                Jump();
            }
            else _localPathFinder.Setup();

        }

    }


    public void Jump()
    {
        Debug.Log("Jump");
        _idRegion++;
        _localPathFinder.Jump(_path[IDRegion].GetComponent<JumpRegion>().dotsJump);
        SetupGlobalRegionInLocalPathFinder(IDRegion);
        _localPathFinder.Setup();

    }



    public void SetupGlobalRegionInLocalPathFinder(int id)
    {
        //if (_regions[id] != _regoinEndPoint)
        //{
            //if (id != _regions.Length)
                _localPathFinder.Setup(_path[id], _path[id + 1]);
        //}
    }

    public GameObject SetupStartPositionInLocalPathFinder()
    {
        return _startPoint;
    }

    public void CreatePath()
    {
        //Debug.Log("Start");
        Queue<GlobalRegion> nodes = new Queue<GlobalRegion>();
        List<GlobalRegion> regions = new List<GlobalRegion>();
        GlobalRegion _start = _regionStartPoint;
        nodes.Enqueue(_start);
        regions.Add(_start);

        //while(nodes.Count != 0)
        for (int i = 0; i < 10; i++)
        {
            GlobalRegion current = nodes.Dequeue();
            //  Если достали целевую - можно заканчивать (это верно и для A*)
            if (current == _regionEndPoint) break;
            //Debug.Log("Взяли" +current.name);
            var neighbours = current.GetNeighbours(regions);
            //Debug.Log(neighbours.Count);
            foreach (var node in neighbours)
            {
                regions.Add(node);
                //Debug.Log(node);
                nodes.Enqueue(node);
            }

        }

        foreach (GlobalRegion obj in regions)
        {
            _path.Add(obj);
        }
    }

    private void AppointRegion(bool isStartPoint, bool isEndPoint, GlobalRegion reg)
    {
        if (isStartPoint == true)
        {
            _regionStartPoint = reg;
        }
        if (isEndPoint == true)
        {
            _regionEndPoint = reg;
        }
    }


    private void ImportanceRegion()
    {
        GlobalRegion currentRegion=_regionEndPoint;
        GlobalRegion constRegion=currentRegion;
        int koef=0;
        currentRegion.importance=koef;
        koef++;
        for(int d=0;d<100;d++)
        {
            for(int i=0;i<currentRegion._neighboursRegion.Length;i++)
            {
                if(currentRegion._neighboursRegion[i].importance==-1)
                currentRegion._neighboursRegion[i].importance=koef;
            }
            koef++;
            if(currentRegion._neighboursRegion.Length==1)
            {
                constRegion=currentRegion._neighboursRegion[0];
            }
            int count=0;
            for(int i=0;i<currentRegion._neighboursRegion.Length;i++)
            {
                if(currentRegion._neighboursRegion[i].importance!=-1)
                count++;
            }
            for(int i=0;i<currentRegion._neighboursRegion.Length;i++)
            {
                if(currentRegion._neighboursRegion[i]._istagged==false)
                {
                    currentRegion=currentRegion._neighboursRegion[i];
                   
                    Debug.Log("Region Current"+ currentRegion.name);
                }
            }
            if(count==currentRegion._neighboursRegion.Length)
            {
                currentRegion._istagged=true;
                currentRegion=constRegion;
            }
        }
    }
}
