using UnityEngine;

public class StraightProjectile : Projectile
{
    protected override void Start()
    {
        base.Start();
    }
    
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        rb.linearVelocity = transform.forward * speed;
    }
}
