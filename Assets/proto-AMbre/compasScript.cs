using System;
using UnityEngine;

public class CompasScript : MonoBehaviour
{
    [SerializeField] private GameObject point1;
    [SerializeField] private GameObject point2;
    [SerializeField] private float dist;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 point2pos;

    private void Start()
    {
        point2pos = Vector3.right * dist;
        point1.transform.localPosition = Vector3.zero;
        point2.transform.localPosition = point2pos;
    }

    private void Update()
    {
        dist += Input.mouseScrollDelta.y;
        dist = Mathf.Clamp(dist, 0, Mathf.Infinity);
        point2pos = Vector3.right * dist;
        

        if (point1.transform.localPosition != Vector3.zero)
        {
            point1.transform.localPosition = point2pos * Mathf.Sign(point1.transform.localPosition.x);
        }
        else
        {
            point2.transform.localPosition = point2pos * Mathf.Sign(point2.transform.localPosition.x); 
        }
        
        if (Input.GetMouseButton(0))
        {
            transform.Rotate(Vector3.up,speed);
        }
        else if (Input.GetMouseButton(1))
        {
            transform.Rotate(Vector3.up,-speed);
        }
        
        if (Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(0))
        {
            var pos = point2.transform.position;
            transform.position = pos;
            pos = point2.transform.localPosition;
            point2.transform.localPosition = point1.transform.localPosition;
            point1.transform.localPosition = -pos.normalized*dist;
            
        }
        
        
    }
}
