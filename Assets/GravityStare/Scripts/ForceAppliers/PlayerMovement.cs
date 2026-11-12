using System;
using UnityEngine;

public class PlayerMovement : ForceApplier
{
    [SerializeField] private Player player;
    [SerializeField] private ForceReceiver receiver;
    
    private void FixedUpdate()
    {
        CreateAndApplyForce(receiver);
    }

    public override void CreateAndApplyForce(ForceReceiver receiver)
    {
        force = Vector3.zero;
        if (player.isGrounded)
        {
            force = new Vector3(Input.GetAxis("Horizontal") * transform.forward.x, 0, Input.GetAxis("Vertical") * transform.forward.z) * (Time.deltaTime * power);
        }
        ApplyForce(receiver, force);
    }
}
