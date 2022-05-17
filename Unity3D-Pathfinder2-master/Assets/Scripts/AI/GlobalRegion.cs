using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalRegion : MonoBehaviour
{
    public GlobalRegion[] _neighboursRegion;

    public int importance=-1;

    public bool _istagged=false;
    
    public List<GameObject> _nodeRegion=new List<GameObject>();
    [SerializeField] private float _checkDist=5;

    public bool IsJumpRegion;


    public GameObject[,] matricNodeRegion;

    public bool CheckDotInRegion(Vector3 point)
    {
        
        if(gameObject.TryGetComponent(out BoxCollider box))
        {
            Vector3 closestPoint=box.ClosestPoint(point);
            if(Vector3.Distance(closestPoint,point)<_checkDist)
            {
            return true;
            }
            else
            {
                return false;
            }
        }
        else if(gameObject.TryGetComponent(out SphereCollider sphera))
        {
            Vector3 closestPoint=sphera.ClosestPoint(point);
            if(Vector3.Distance(closestPoint,point)<_checkDist)
            {
            return true;
            }
            else
            {
                return false;
            }
        }
        return false;
               
    }

    
    public  List<GlobalRegion> GetNeighbours(List<GlobalRegion> queueRegion)
    {
        List<GlobalRegion> withoutRepeatNeighbours=new List<GlobalRegion>();

        
       

        for(int i=0;i< _neighboursRegion.Length;i++)
        {
            bool isNotfind=false;
            for(int j=0;j< queueRegion.Count;j++)
            {
                if(queueRegion[j] ==_neighboursRegion[i])
                {
                    isNotfind=true;
                }
            }
            if(isNotfind==false)
            {
                withoutRepeatNeighbours.Add(_neighboursRegion[i]);
               
            }
        }
    
        return withoutRepeatNeighbours;
    }
   

   public void SortDots()
   {
       
   }
    
}
