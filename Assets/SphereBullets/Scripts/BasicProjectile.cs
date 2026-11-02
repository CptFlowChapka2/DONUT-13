using UnityEngine;

public class BasicProjectile : Projectile
{
    private float t;
    
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
        
        if (heightBounciness <= 0 || lengthBounciness <= 0) return;
        
        t += Time.fixedDeltaTime;
        if (t >= lengthBounciness) t = 0;
        distanceToSphere = 5 + ((4 * heightBounciness) / (lengthBounciness * lengthBounciness)) * t *
            (lengthBounciness - t);
    }
}
