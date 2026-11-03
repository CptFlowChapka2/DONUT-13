using System;
using UnityEngine;

public class ProjectileCameraBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private float sensibilityZoom;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;

    private float camDistance;

    private void Start()
    {
        camDistance = minDistance;
    }

    private void Update()
    {
        if (!cam.activeSelf) return;
        Zoom();
    }

    private void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            camDistance -= Input.GetAxis("Mouse ScrollWheel") * sensibilityZoom;
            camDistance = Mathf.Clamp(camDistance, minDistance, maxDistance);
        }
        cam.transform.position = camDistance * transform.up;
    }
}
