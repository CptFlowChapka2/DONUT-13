using System;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public Camera personalCamera;
    public Rigidbody rb;
    public float distanceToSphere;
    public float speed;
    public float rotationSpeed;
    [Header("Bounciness")]
    public float heightBounciness;
    public float lengthBounciness;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void FixedUpdate()
    {
        Vector3 normal = transform.position.normalized;
        transform.position = normal * distanceToSphere;
        Vector3 dirToForward = Vector3.ProjectOnPlane(transform.forward, normal).normalized;
        if (dirToForward.sqrMagnitude <= 0.0001f) return;
        transform.rotation = Quaternion.LookRotation(dirToForward, normal);
    }
}
