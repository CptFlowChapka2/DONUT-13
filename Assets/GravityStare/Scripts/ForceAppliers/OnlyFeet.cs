using System;
using System.Linq;
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
        player.playerState = PlayerState.Grounded;
        force = Vector3.zero;
        receiver.forces.Add(this, force);
    }

    private void OnCollisionStay(Collision other)
    {
        if (!other.collider.CompareTag("Ground")) return;
        player.playerState = PlayerState.Grounded;
        
        var normal = Vector3.zero;
        foreach (var otherContact in other.contacts)
        {
            normal += otherContact.normal;
        }

        Debug.DrawLine(other.contacts[0].point,normal*5,Color.red,Mathf.Infinity);
        normal = normal / other.contactCount;
        rb.transform.up = normal.normalized;
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
        Debug.Log(force);
    }

    public override void AddOrToggleReceiver(ForceReceiver r, bool togleMode)
    {
        throw new NotImplementedException();
    }
}
