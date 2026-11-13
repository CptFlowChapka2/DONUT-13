using System;
using UnityEngine;

public class OnlyFeet : ForceApplier
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private ForceReceiver receiver;
    [SerializeField] private ForceApplier ownMovement;
    [SerializeField] private Player player;
    [SerializeField] private float staticForceMultiplier;

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
        player.isGrounded = true;
        rb.transform.up = other.contacts[0].normal;
        CreateAndApplyForce(receiver);
    }

    private void OnCollisionExit(Collision other)
    {
        if (!other.collider.CompareTag("Ground")) return;
        receiver.forces.Remove(this);
    }

    public override void CreateAndApplyForce(ForceReceiver receiver)
    {
        if (InputSystemManager.instance.movementAction.ReadValue<Vector2>() != Vector2.zero) return;
        force = -rb.linearVelocity * (Time.fixedDeltaTime * power);
        ApplyForce(receiver, force);
    }

    public override void AddOrToggleReceiver(ForceReceiver r, bool togleMode)
    {
        throw new NotImplementedException();
    }
}
