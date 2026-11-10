using System;
using UnityEngine;

public class PlayerMovement : ForceApplier
{
    [SerializeField] private Player player;
    [SerializeField] private ForceReceiver receiver;

    private void FixedUpdate()
    {
        Vector3 movement = Vector3.zero;
        if (player.isGrounded)
        {
            movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * (Time.deltaTime * power);
        }
        ApplyForce(receiver, movement);
    }
}
