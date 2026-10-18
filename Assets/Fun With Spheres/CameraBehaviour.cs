using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private Transform camera;
    [SerializeField] private float maxDistanceToSphere = 20.0f;
    [SerializeField] private float minDistanceToSphere = 12.0f;
    [SerializeField] private float sensibilityZoom = 1f;
    [SerializeField] private float sensibilityRotation = 1f;
    private float distanceToSphere = 18.0f;
    private bool isDragging = false;

    private void Update()
    {
        Move();
        Zoom();
        camera.transform.position = transform.position + (camera.transform.position - transform.position).normalized * distanceToSphere;
    }

    private Vector2 previousPosition = Vector2.zero;
    
    private void Move()
    {
        if (Input.GetMouseButtonDown(2))
        {
            previousPosition = Input.mousePosition;
            return;
        }
        if (Input.GetMouseButtonUp(2))
        {
            previousPosition = Vector2.zero;
            return;
        }

        if (Input.GetMouseButton(2))
        {
            Vector2 nextPosition = Input.mousePosition;
            Vector2 delta = nextPosition - previousPosition;
            transform.Rotate(Vector3.up, delta.x * sensibilityRotation);
            transform.Rotate(Vector3.right, -delta.y * sensibilityRotation);
            previousPosition = nextPosition;
        }
    }
    
    private void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            distanceToSphere -= Input.GetAxis("Mouse ScrollWheel") * sensibilityZoom;
            distanceToSphere = Mathf.Clamp(distanceToSphere, minDistanceToSphere, maxDistanceToSphere);
        }
    }
}
