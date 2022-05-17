using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpRegion : MonoBehaviour
{
    [SerializeField] private GameObject _platforma;
    
    [SerializeField] private Vector3 _transformPlatfom;
    public GameObject dotsJump;

    public void SearchDotsJump()
    {
        List<GameObject> dotsRegion=GetComponent<GlobalRegion>()._nodeRegion;

        float minDistance=1000;
        foreach(GameObject jumpDots in dotsRegion)
        {
            
            if(Vector3.Distance(_transformPlatfom,jumpDots.transform.position)<minDistance)
            {
                
                dotsJump=jumpDots;
                minDistance=Vector3.Distance(_transformPlatfom,jumpDots.transform.position);
                // Debug.Log(jumpDots.name+" distance  " + minDistance);
            }
        }
    }
}
