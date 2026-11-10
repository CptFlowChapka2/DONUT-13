using System;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : ForceApplier
{
    private HashSet<ForceReceiver> receivers = new HashSet<ForceReceiver>();

    private void Start()
    {
        influenceRadius = 1 + 0.7f * power;
        SphereCollider sphereCollider = collider as SphereCollider;
        sphereCollider.radius = influenceRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Forceless")) return;
        receivers.Add(CollidersHolder.FindReceiverByCollider(other));
    }

    private void FixedUpdate()
    {
        foreach (ForceReceiver receiver in receivers)
        {
            Vector3 direction = (transform.position - receiver.transform.position).normalized * (power * Time.fixedDeltaTime);
            ApplyForce(receiver, direction);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Forceless")) return;
        CollidersHolder.FindReceiverByCollider(other).forces.Remove(this);
        receivers.Remove(CollidersHolder.FindReceiverByCollider(other));
    }

    // private void OnTriggerStay(Collider other)
    // {
    //     var dist = transform.position - other.transform.position;
    //     force = dist;
    //     other.attachedRigidbody ?.AddForce(dist,ForceMode.Force);
    // }
}
