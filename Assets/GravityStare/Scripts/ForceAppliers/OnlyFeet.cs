using System;
using UnityEngine;

public class OnlyFeet : ForceApplier
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private ForceReceiver receiver;
    [SerializeField] private ForceApplier ownMovement;
    [SerializeField] private Player player;
    [SerializeField] private float staticForceMultplier;

    private void OnCollisionEnter(Collision other)
    {
        if (!other.collider.CompareTag("Ground")) return;
        player.isGrounded = true;
        force = Vector3.zero;
        receiver.forces.Add(this, force);
    }

    private void OnCollisionStay(Collision other)
    {
        if (!other.collider.CompareTag("Ground")) return;
        rb.transform.up = other.contacts[0].normal.normalized;
        
        CreateAndApllyForce(receiver);
    }

    private void OnCollisionExit(Collision other)
    {
        if (!other.collider.CompareTag("Ground")) return;
        player.isGrounded = false;
        receiver.forces.Remove(this);
    }

    public override void CreateAndApllyForce(ForceReceiver receiver)
    {
        force = -rb.linearVelocity * (Time.fixedDeltaTime * power);
        if (ownMovement.force.magnitude <= 0) force *= staticForceMultplier;
        ApplyForce(receiver, force);
    }
}
