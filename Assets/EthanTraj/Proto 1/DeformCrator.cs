using System;
using UnityEngine;

public class DeformCrator : MonoBehaviour
{
    public GameObject deformer;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Clicked();
    }

    void Clicked()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast (ray, out hit))
        {
            if (hit.collider.CompareTag("DSurface"))
            {
                GameObject d = Instantiate(deformer);
                d.transform.position = hit.point;
            }
        }
    }
}
