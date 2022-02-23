using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KevRoute : MonoBehaviour
{
    [SerializeField]  private int time;
    [SerializeField]  private Transform[] routePoints;
    private Vector3 gizmoPos;

    private void OnDrawGizmos()
    {
        for (float t = 0; t <= 1; t += 0.05f)
        {
            gizmoPos = Mathf.Pow(1 - t, 3) * routePoints[0].position + 3 * Mathf.Pow(1 - t, 2) * t * routePoints[1].position + 3 * (1 - t) * Mathf.Pow(t, 2) * routePoints[2].position + Mathf.Pow(t, 3) * routePoints[3].position;
            Gizmos.DrawSphere(gizmoPos, 1f);
        }
       
    }
    
}
