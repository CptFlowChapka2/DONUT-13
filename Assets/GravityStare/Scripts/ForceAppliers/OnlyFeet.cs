using System;
using UnityEngine;

public class OnlyFeet : ForceApplier
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private ForceReceiver receiver;
    [SerializeField] private Player player;

    private void OnCollisionEnter(Collision other)
    {
        if (!other.collider.CompareTag("Ground")) return;
        player.isGrounded = true;
        Vector3 friction = Vector3.zero;
        receiver.forces.Add(this, friction);
    }

    private void OnCollisionStay(Collision other)
    {
        if (!other.collider.CompareTag("Ground")) return;
        rb.transform.up = other.contacts[0].normal.normalized;
        
        Vector3 friction = -rb.linearVelocity * (Time.fixedDeltaTime * power);
        ApplyForce(receiver, friction);
    }

    private void OnCollisionExit(Collision other)
    {
        if (!other.collider.CompareTag("Ground")) return;
        player.isGrounded = false;
        receiver.forces.Remove(this);
    }
}
