using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum Handedness
{
    Left = 1,
    Right = -1,
}

public class OrthogonalCoodinateSystem : MonoBehaviour
{
    [SerializeField] private Vector3 v;
    [SerializeField] private Vector3 w;
    [SerializeField] private Handedness handedness;
    [SerializeField] private bool forceOrthogonalSystem;

    private void OnDrawGizmos()
    {
        var v1 = v;
        var v2 = w;
        var v3 = Vector3.Cross(v, w) * ConvertEnumToInt(handedness);

        if (forceOrthogonalSystem)
        {
            v1.Normalize();
            v3.Normalize();
            v2 = (Vector3.Cross(v3, v1) * ConvertEnumToInt(handedness)).normalized;
        }
        
        Gizmos.color = Color.yellow;
        GizmosUtils.DrawVectorAtOrigin(v1);
        
        Gizmos.color = Color.green;
        GizmosUtils.DrawVectorAtOrigin(v2);
        
        Gizmos.color = Color.red;
        GizmosUtils.DrawVectorAtOrigin(v3);
    }

    private int ConvertEnumToInt(Handedness hand)
    {
        return (int) hand;
    }
    
}
