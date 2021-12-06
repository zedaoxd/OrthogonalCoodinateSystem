using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum Handedness
{
    Left = 0,
    Right = 1,
}

public class OrthogonalCoodinateSystem : MonoBehaviour
{
    [SerializeField] private Vector3 v;
    [SerializeField] private Vector3 w;
    [SerializeField] private Handedness handedness;
    [SerializeField] private bool forceOrthogonalSystem;

    private void OnDrawGizmos()
    {
        DrawVectors();
    }

    private void DrawVectors ()
    {
        var croosProduct = CrossProduct(v, w);
        if (forceOrthogonalSystem)
        {
            croosProduct = GramSchmidtOrthogonalization(v, w, croosProduct);
            HandednessType(croosProduct);
            return;
        }
        
        Gizmos.color = Color.red;
        GizmosUtils.DrawVectorAtOrigin(v);
        Gizmos.color = Color.green;
        GizmosUtils.DrawVectorAtOrigin(w);
        HandednessType(croosProduct);
    }

    private static Vector3 CrossProduct(Vector3 v, Vector3 w)
    {
        return new Vector3()
        {
            x = v.y * w.z - v.z * w.y,
            y = v.z * w.x - v.x * w.z,
            z = v.x * w.y - v.y * w.x,
        };
    }

    private void HandednessType(Vector3 croosProduct)
    {
        Gizmos.color = Color.yellow;
        GizmosUtils.DrawVectorAtOrigin(handedness == Handedness.Left ? croosProduct : -croosProduct);
    }

    private Vector3 GramSchmidtOrthogonalization(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        Vector3 w1 = v1;
        Vector3 w2 = v2 - (Vector3.Dot(v2, w1) / Vector3.Dot(w1, w1)) * w1;
        Vector3 w3 = v3 - (Vector3.Dot(v3, w2) / Vector3.Dot(w2, w2)) * w2 -
                     (Vector3.Dot(v3, w1) / Vector3.Dot(w1, w1)) * w1;
        
        Gizmos.color = Color.red;
        GizmosUtils.DrawVectorAtOrigin(w1 / w1.magnitude);
        Gizmos.color = Color.green;
        GizmosUtils.DrawVectorAtOrigin(w2 / w2.magnitude);
        return w3 / w3.magnitude;
    }
}
