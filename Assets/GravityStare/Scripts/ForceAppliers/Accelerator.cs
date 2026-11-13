using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Accelerator : ForceApplier
{
    private void Start()
    {
        force = Random.onUnitSphere;
    }

    private void OnTriggerEnter(Collider other)
    {
        CollidersHolder.FindReceiverByCollider(other).forces
            .TryAdd(this, force.normalized * power * Time.fixedDeltaTime);
    }

    private void OnTriggerExit(Collider other)
    {
        CollidersHolder.FindReceiverByCollider(other).forces.Remove(this);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, force.normalized * power);
    }

    public override void CreateAndApplyForce(ForceReceiver receiver)
    {
        throw new NotImplementedException();
    }

    public override void AddOrToggleReceiver(ForceReceiver r, bool togleMode)
    {
        throw new NotImplementedException();
    }
}

// //other.attachedRigidbody ?.AddForce(transform.forward*power,ForceMode.Force);
// var rota = Quaternion.RotateTowards(other.attachedRigidbody.rotation, 
//     Quaternion.FromToRotation(other.attachedRigidbody.linearVelocity, transform.forward), 
//     power * Time.fixedDeltaTime);
//         
// other.attachedRigidbody.linearVelocity = rota * other.attachedRigidbody.linearVelocity ;
// //other.GetComponent<bullet>()
