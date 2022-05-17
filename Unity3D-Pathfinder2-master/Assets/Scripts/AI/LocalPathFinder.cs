using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class LocalPathFinder : MonoBehaviour
{
    [SerializeField] private GlobalRegion _startRegion;
    [SerializeField] private GlobalRegion _endRegion;

    [SerializeField] private GameObject _startPosition;

    [SerializeField] private GameObject _endPosition;

    [SerializeField] private Grid _grid;

    [SerializeField] private float _distanceBetweenNode = 5f;

    [SerializeField] private List<GameObject> _fullPath=new List<GameObject>();

    public UnityEvent EventUpdateRegion;

    public GameObject FindNearDotsInGridSmoothSurface(List<GameObject> node, GameObject position)
    {
        float minDist = 100000;
        int idMinDist = -1;
        int counter = 0;
        foreach (GameObject obj in node)
        {
            if (minDist >= Vector3.Distance(obj.transform.position, position.transform.position))
            {
                idMinDist = counter;
                // Debug.Log(minDist);
                //  Debug.Log(idMinDist);
                minDist = Vector3.Distance(obj.transform.position, position.transform.position);
            }
            counter++;
        }

        return node[idMinDist];
    }

    public void SetupStartPosition(GameObject startPosition)
    {
        _startPosition = startPosition;
    }

    public void Setup(GlobalRegion startRegion, GlobalRegion endRegion)
    {
        _startRegion = startRegion;
        _endRegion = endRegion;
    }
    public void Setup()
    {
        if (_startRegion.TryGetComponent(out JumpRegion jump))
        {
            _startPosition = jump.dotsJump;
            Debug.Log("точка прыжка найдена");
        }
        else
        {
            _startPosition = FindNearDotsInGridSmoothSurface(_startRegion._nodeRegion, _startPosition);
        }
        if (_endRegion.TryGetComponent(out JumpRegion jumpRegion))
        {
            _endPosition = jumpRegion.dotsJump;
            Debug.Log("точка прыжка найдена");
        }
        else
        {
            _endPosition = FindNearDotsInGridSmoothSurface(_endRegion._nodeRegion, _startPosition);
        }
        // _startPosition = FindNearDotsInGridSmoothSurface(_startRegion._nodeRegion, _startPosition);
        //_endPosition = FindNearDotsInGridSmoothSurface(_endRegion._nodeRegion, _startPosition);
        AStar();
    }
    public void Setup(GlobalRegion endRegion, GameObject end)
    {
        _startRegion = endRegion;
        _endRegion = endRegion;
        _endPosition = end;
        AStar();
    }



    private List<GameObject> UnionTwoRegions()
    {
        List<GameObject> twoRegionsNode = new List<GameObject>();
        for (int i = 0; i < _startRegion._nodeRegion.Count; i++)
        {
            twoRegionsNode.Add(_startRegion._nodeRegion[i]);
        }
        for (int i = 0; i < _endRegion._nodeRegion.Count; i++)
        {
            twoRegionsNode.Add(_endRegion._nodeRegion[i]);
        }
        return twoRegionsNode;

    }

    private List<GameObject> GetNeighbours(List<GameObject> twoRegionsNode, GameObject current)
    {
        List<GameObject> neighbours = new List<GameObject>();
        foreach (GameObject obj in twoRegionsNode)
        {
            if (Vector3.Distance(current.transform.position, obj.transform.position) == _distanceBetweenNode)
            {
                neighbours.Add(obj);
            }
            if (neighbours.Count == 4) break;
        }
        Debug.Log("Соседи" + neighbours.Count);
        return neighbours;
    }


    public void AStar()
    {

        List<GameObject> twoRegionNode = UnionTwoRegions();
        List<GameObject> path = new List<GameObject>();
        //Priority_Queue.FastPriorityQueue<PNode> pq = new Priority_Queue.FastPriorityQueue<PNode>(twoRegionNode.Count);
        Queue<GameObject> nodes = new Queue<GameObject>();
        nodes.Enqueue(_startPosition);
        path.Add(_startPosition);
        _fullPath.Add(_startPosition);
        Debug.Log("Test");
        Debug.Log("StarPosition " + _startPosition.name + " StartRegion " + _startRegion.name);
        Debug.Log("EndPosition " + _endPosition.name+ " EndRegion " + _endRegion.name);
        while (nodes.Count > 0)
        {
            GameObject current = nodes.Dequeue();
            float minDist = 1000;
            //  Если достали целевую - можно заканчивать (это верно и для A*)
            if (current == _endPosition) break;
            //Debug.Log("Взяли" +current.name);
            var neighbours = GetNeighbours(twoRegionNode, current);
            //Debug.Log(neighbours.Count);
            foreach (var node in neighbours)
            {
                float distCurrentAndFinish = Vector3.Distance(current.transform.position, _endPosition.transform.position);
                float distNodeAndFinish = Vector3.Distance(node.transform.position, _endPosition.transform.position);
                /*  Debug.Log("Node "+node.name);
                  Debug.Log("Current "+current.name);
                  Debug.Log("distCurrentAndFinish "+ distCurrentAndFinish);
                  Debug.Log("distNodeAndFinish "+ distNodeAndFinish);
                  */
                if ((distCurrentAndFinish > distNodeAndFinish && distNodeAndFinish < minDist) && node.GetComponent<WalkableNode>().GetIsWalkable()==true)
                {

                    Debug.Log("Добавлено" + node.name+ " "+ neighbours.Count+" "+node.GetComponent<WalkableNode>().GetIsWalkable().ToString());
                    //Debug.Log("Добавлено distCurrentAndFinish" + distCurrentAndFinish);
                    //Debug.Log("Добавлено distNodeAndFinish" + distNodeAndFinish);
                    minDist = distNodeAndFinish;
                    path.Add(node);
                     _fullPath.Add(node);
                    nodes.Enqueue(node);
                    break;
                }
            }

        }
        //Debug.Log("Nodes" +path.Count);
        for (int i = 0; i < path.Count; i++)
        {
            // Debug.Log("Nodes+name" +path[i].name);
            path[i].GetComponent<Renderer>().material.color = Color.white;
        }
        _startPosition = _endPosition;
        EventUpdateRegion?.Invoke();
    }


    public void Jump(GameObject startpoint)
    {
        _startPosition = startpoint;
    }

    public List<GameObject> GetFullPath()
    {
        return _fullPath;
    }


}
