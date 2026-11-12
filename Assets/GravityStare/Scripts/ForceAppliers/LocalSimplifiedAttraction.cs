using System;
using System.Collections.Generic;
using UnityEngine;

public class LocalSimplifiedAttraction : ForceApplier
{
    private void Start()
    {
        influenceRadius = 1 + 5f;
        SphereCollider sphereCollider = collider as SphereCollider;
        sphereCollider.radius = influenceRadius;
    }

    public override void CreateAndApplyForce(ForceReceiver receiver)
    {
        Vector3 direction = (transform.position - receiver.transform.position).normalized * (power * Time.fixedDeltaTime);
        ApplyForce(receiver, direction);
    }

    public override void AddOrToggleReceiver(ForceReceiver r,bool toggleMode)
    {
        if (toggleMode)
        {
            //if(collider.bounds.Contains(r.transform.position))return;
            if (receivers.Contains(r)) receivers.Remove(r);
            else receivers.Add(r);
        }
        else
        {
            receivers.Add(r);
        }
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
            CreateAndApplyForce(receiver);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Forceless")) return;
        var r = CollidersHolder.FindReceiverByCollider(other);
        r.forces.Remove(this);
        receivers.Remove(r);
    }

    // private void OnTriggerStay(Collider other)
    // {
    //     var dist = transform.position - other.transform.position;
    //     force = dist;
    //     other.attachedRigidbody ?.AddForce(dist,ForceMode.Force);
    // }
}
