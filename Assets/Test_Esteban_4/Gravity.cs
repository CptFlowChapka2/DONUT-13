using System;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        var dist = transform.position - other.transform.position;
        
        other.attachedRigidbody ?.AddRelativeForce(dist,ForceMode.Force);
    }
}
