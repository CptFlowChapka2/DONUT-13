using System;
using UnityEngine;

public class LocalSimplifiedReaction : ForceApplier
{
    [SerializeField] private LocalSimplifiedAttraction simplifiedAttraction;

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Forceless")) return;
        Debug.Log("enter");
        receivers.Add(CollidersHolder.FindReceiverByCollider(other.collider));
    }

    private void OnCollisionStay(Collision other)
    {
        // other.transform.up = -other.contacts[0].normal;
        // foreach (ForceReceiver receiver in receivers)
        // {
        //     CreateAndApplyForce(receiver);
        // }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Forceless")) return;
        Debug.Log("exit");
        var r = CollidersHolder.FindReceiverByCollider(other.collider);
        r.forces.Remove(this);
        receivers.Remove(r);
    }

    public override void CreateAndApplyForce(ForceReceiver receiver)
    {
        Vector3 reaction = -receiver.forces[simplifiedAttraction];
        ApplyForce(receiver, reaction);
    }

    public override void AddOrToggleReceiver(ForceReceiver r, bool togleMode)
    {
        throw new NotImplementedException();
    }
}
