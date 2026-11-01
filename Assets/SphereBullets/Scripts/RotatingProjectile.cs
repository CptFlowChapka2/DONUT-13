using UnityEngine;

public class RotatingProjectile : Projectile
{
    public float rotationSpeed;
    
    protected override void Start()
    {
        base.Start();
    }
    
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        rb.linearVelocity = transform.forward * speed;
        
        Vector3 normal = transform.position.normalized;
        Vector3 currentForward = Vector3.ProjectOnPlane(transform.forward, normal).normalized;
        Vector3 direction = Quaternion.AngleAxis(rotationSpeed, normal) * currentForward;
        if (direction.sqrMagnitude <= 0.0001f) return;
        transform.rotation = Quaternion.LookRotation(direction, normal);
    }
}
