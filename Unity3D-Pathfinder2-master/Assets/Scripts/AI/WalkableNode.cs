using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkableNode : MonoBehaviour
{

    [SerializeField] private bool _isWalkable = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            _isWalkable = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            _isWalkable = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            _isWalkable = true;
        }
    }

    public bool GetIsWalkable()
    {
        return _isWalkable;
    }

}
