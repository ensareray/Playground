﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGround : MonoBehaviour
{
    [SerializeField] float minX,maxX,minZ,maxZ;
    public bool CheckInPlayground(Transform t)
    {
        if(t.position.x > minX && t.position.x < maxX && t.position.z > minZ && t.position.z < maxZ)
        {
            return true;
        }
        else
        {
            Debug.Log("Not in limit: "+ t.name);
            return false;
        }
    }
    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.DrawCube(new Vector3(minX,0,0),Vector3.one * 3);
        Gizmos.DrawCube(new Vector3(maxX,0,0),Vector3.one * 3);
        Gizmos.DrawCube(new Vector3(0,0,minZ),Vector3.one * 3);
        Gizmos.DrawCube(new Vector3(0,0,maxZ),Vector3.one * 3);
    }
}