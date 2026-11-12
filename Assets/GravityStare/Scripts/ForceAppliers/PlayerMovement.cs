using System;
using UnityEngine;

public class PlayerMovement : ForceApplier
{
    [SerializeField] private Player player;
    [SerializeField] private ForceReceiver receiver;

    private void FixedUpdate()
    {
        CreateAndApllyForce(receiver);
    }

    public override void CreateAndApllyForce(ForceReceiver receiver)
    {
        force = Vector3.zero;
        if (player.isGrounded)
        {
            force = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * (Time.deltaTime * power);
        }
        ApplyForce(receiver, force);
    }
}
