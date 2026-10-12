using System;
using System.Linq;
using UnityEngine;

public class CheckRendered : MonoBehaviour
{
    // public Renderer selfRenderer;
    public Transform target;
    private Camera playerCam;

    private void Start()
    {
        playerCam = Camera.main;
    }

    void Update()
    {
        if (IsVisible())
        {
            if (Physics.Raycast(transform.position, target.position, out RaycastHit hit))
            {
                Debug.Log(hit.collider.tag);
                Debug.DrawRay(transform.position, target.position, Color.blue);
                
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
