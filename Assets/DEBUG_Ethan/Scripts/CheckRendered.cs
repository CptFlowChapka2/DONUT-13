using System;
using System.Linq;
using UnityEngine;

public class CheckRendered : MonoBehaviour
{
    public Transform target;
    private Camera playerCam;

    private void Start()
    {
        playerCam = Camera.main;
    }

    void Update()
    {
        Debug.DrawRay(transform.position, (target.position - transform.position), Color.blue);
        
        if (IsVisible())
        {
            if (Physics.Raycast(transform.position, (target.position - transform.position), out RaycastHit hit))
            {
                if(hit.collider.CompareTag("Target"))
                    Debug.Log("Visible");
                
                else Debug.Log("VISIBLE BUT HIDED");
            }
        }
            
        else Debug.Log("NOT VISIBLE");
    }

    bool IsVisible()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(playerCam);
        return planes.All(plane => plane.GetDistanceToPoint(target.position) >= 0);
    }
}
