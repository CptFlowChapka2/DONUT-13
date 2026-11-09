using System;
using UnityEngine;

public class CompasScript : MonoBehaviour
{
    [SerializeField] private GameObject point1;
    [SerializeField] private GameObject point2;
    [SerializeField] private float dist;
    [SerializeField] private float speed;

    private void Start()
    {
        point1.transform.localPosition = Vector3.zero;
        point2.transform.localPosition = new Vector3(dist,0,0);
    }

    private void Update()
    {
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
            point1.transform.localPosition = -pos;
            
        }
    }
}
