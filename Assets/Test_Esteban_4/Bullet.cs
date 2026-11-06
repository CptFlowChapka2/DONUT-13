using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    
    public Dictionary<ForceApplier, Vector3> forces = new Dictionary<ForceApplier, Vector3>();
    
    private void Start()
    {
        CollidersHolder.instance.colliders.Add((this, this.GetComponent<Collider>()));
        rb.linearVelocity = transform.forward * speed;
    }

    private void FixedUpdate()
    {
        Vector3 appliedForce = rb.linearVelocity;
        foreach (var force in forces.Values)
        {
            appliedForce += force;
        }
        rb.linearVelocity = appliedForce;
    }
}
