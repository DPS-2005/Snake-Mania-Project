using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMarker : MonoBehaviour
{
    public Color color;
    private void OnDrawGizmos()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            Debug.DrawLine(transform.position, hit.point, color);
            Gizmos.color = color;
            Gizmos.DrawWireSphere(hit.point, 0.5f);
        }
    }
}
