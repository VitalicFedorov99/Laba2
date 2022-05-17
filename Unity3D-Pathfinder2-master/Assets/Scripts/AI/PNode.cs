using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNode : Priority_Queue.FastPriorityQueueNode
{
    public bool walkable;            //  Проходим узел сетки
    public Vector3 worldPosition;    //  Координаты узла
    public GameObject body;         //  Тело узла
    private PNode parent = null;  //  Родительский узел
    private float distance = float.PositiveInfinity;
    public Vector2Int gridIndex;


  
    
    public PNode ParentNode
    {
        get => parent;
        set => SetParent(value);
    }

    private void SetParent(PNode parentNode)
    {
        parent = parentNode;
        if (parent != null)
            distance = parent.distance + PNode.Dist(parent, this);
        else
            distance = float.PositiveInfinity;
    }

    public float Distance
    {
        get => distance;
        set => distance = value;    //   Плохо!
    }

    public PNode(GameObject prefab, bool walkable, Vector3 position,int id,GameObject parent)
    {
        this.walkable = walkable;
        worldPosition = position;
        SetParent(null);
        body = GameObject.Instantiate(prefab, position, Quaternion.identity);
        body.name=body.name+id.ToString();
        body.transform.parent=parent.transform;
        body.GetComponent<PNodeInGlobalRegion>().id=id;
        
    }

    public static float Dist(PNode source, PNode dest)
    {
        float baseDist = Vector3.Distance(source.worldPosition, dest.worldPosition);
        if (dest.worldPosition.y > source.worldPosition.y)
            return baseDist + 10 * (dest.worldPosition.y - source.worldPosition.y);
        else
            //return baseDist + (dest.worldPosition.y - source.worldPosition.y);
            return baseDist;
    }

    public void Illuminate()
    {
        body.GetComponent<Renderer>().material.color = Color.blue;
    }

    public void Fade()
    {
        body.GetComponent<Renderer>().material.color = Color.gray;
    }

    public void Highlight()
    {
        body.GetComponent<Renderer>().material.color = Color.red;
    }
}
