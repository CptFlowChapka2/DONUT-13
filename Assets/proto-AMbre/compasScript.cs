using System;
using UnityEngine;

public class CompasScript : MonoBehaviour
{
    [SerializeField] private GameObject point1;
    [SerializeField] private GameObject point2;
    [SerializeField] private float dist;
    private float angle;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 point2pos;

    [SerializeField] private float maxDist;
    [SerializeField] private float maxAngle;
    
    private void Start()
    {
        point2pos = Vector3.right * dist;
        point1.transform.localPosition = Vector3.zero;
        point2.transform.localPosition = point2pos;
    }

    private void Update()
    {
        dist += Input.mouseScrollDelta.y;
        dist = Mathf.Clamp(dist, 1, maxDist);
        
        point2pos = Vector3.right * dist;

        if (point1.transform.localPosition != Vector3.zero)
        {
            point1.transform.localPosition = point2pos * Mathf.Sign(point1.transform.localPosition.x);
        }
        else
        {
            point2.transform.localPosition = point2pos * Mathf.Sign(point2.transform.localPosition.x); 
        }
        
        // if (Input.GetMouseButton(0))
        // {
        //     transform.Rotate(Vector3.up,speed);
        // }
        // else if (Input.GetMouseButton(1))
        // {
        //     transform.Rotate(Vector3.up,-speed);
        // }

        float delta = 0f;

        if (Input.GetMouseButton(0))
        {
            delta = speed * 60 * Time.deltaTime;
        }
        else if (Input.GetMouseButton(1))
        {
            delta = -speed * 60 * Time.deltaTime;
        }

        float ancienAngle = angle;
        angle = Mathf.Clamp(angle + delta, -maxAngle, maxAngle);
        transform.Rotate(Vector3.up, angle - ancienAngle);
        
        if (Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(0))
        {
            bool point1EstPivot = point1.transform.localPosition == Vector3.zero;
            GameObject mobile = point1EstPivot ? point2 : point1;
            GameObject pivot  = point1EstPivot ? point1 : point2;

            Vector3 offset = mobile.transform.localPosition;
            transform.position = mobile.transform.position; 
            mobile.transform.localPosition = Vector3.zero;  
            pivot.transform.localPosition = -offset;   

            angle = 0f;
            
            // var pos = point2.transform.position;
            // transform.position = pos;
            // pos = point2.transform.localPosition;
            // point2.transform.localPosition = point1.transform.localPosition;
            // point1.transform.localPosition = -pos.normalized*dist;
            
        }
        
        
    }
}