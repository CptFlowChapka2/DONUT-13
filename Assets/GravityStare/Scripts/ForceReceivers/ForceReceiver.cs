using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class ForceReceiver : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public float maxSpeed;
    
    public Dictionary<ForceApplier, Vector3> forces = new Dictionary<ForceApplier, Vector3>();
    
    private void Start()
    {
        rb.maxLinearVelocity = maxSpeed;
        CollidersHolder.instance.colliders.Add((this, this.GetComponent<Collider>()));
        rb.linearVelocity = transform.forward * speed;
    }

    private void FixedUpdate()
    {
        Vector3 appliedForce = rb.linearVelocity;
        foreach (var force in forces.Values)
        {
            appliedForce += BiasAddition(force);
        }
        rb.linearVelocity = appliedForce;
    }

    protected abstract Vector3 BiasAddition(Vector3 force);

}
