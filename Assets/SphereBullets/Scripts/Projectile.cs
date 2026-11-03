using System;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public Camera personalCamera;
    public Rigidbody rb;
    public float distanceToSphere;
    public ProjectileData projectileData;

    public float speed;
    public float rotationSpeed;
    private float heightBounciness;
    private float lengthBounciness;
    
    protected float t;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = projectileData.speed;
        rotationSpeed = projectileData.rotationSpeed;
        heightBounciness = projectileData.heightBounciness;
        lengthBounciness = projectileData.lengthBounciness;
    }

    protected virtual void FixedUpdate()
    {
        RecalculateForward();
        ApplyMoveForward();
        ApplyRotation();
        ApplyBounciness();
    }

    private void RecalculateForward()
    {
        Vector3 normal = transform.position.normalized;
        transform.position = normal * distanceToSphere;
        Vector3 dirToForward = Vector3.ProjectOnPlane(transform.forward, normal).normalized;
        if (dirToForward.sqrMagnitude <= 0.0001f) return;
        transform.rotation = Quaternion.LookRotation(dirToForward, normal);
    }

    protected void ApplyBounciness()
    {
        if (heightBounciness <= 0 || lengthBounciness <= 0) return;
        
        t += Time.fixedDeltaTime;
        if (t >= lengthBounciness) t = 0;
        distanceToSphere = 5 + ((4 * heightBounciness) / (lengthBounciness * lengthBounciness)) * t *
            (lengthBounciness - t);
    }

    protected void ApplyRotation()
    {
        if (rotationSpeed == 0) return;
        
        Vector3 normal = transform.position.normalized;
        Vector3 currentForward = Vector3.ProjectOnPlane(transform.forward, normal).normalized;
        Vector3 direction = Quaternion.AngleAxis(rotationSpeed, normal) * currentForward;
        if (direction.sqrMagnitude > 0.0001f) transform.rotation = Quaternion.LookRotation(direction, normal);
    }
    
    protected void ApplyMoveForward()
    {
        rb.linearVelocity = transform.forward * speed;
    }
}
