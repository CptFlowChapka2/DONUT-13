using System;
using UnityEngine;

public class PlayerMovement : ForceApplier
{
    [SerializeField] private Player player;
    [SerializeField] private ForceReceiver receiver;

    private InputSystemManager ism;
    private Vector3 inputLastVerifiedUpdate = Vector3.zero;

    private void Start()
    {
        ism = InputSystemManager.instance;
    }

    private void FixedUpdate()
    {
        CreateAndApplyForce(receiver);
    }

    public override void CreateAndApplyForce(ForceReceiver receiver)
    {
        if (!player.isGrounded) return;
        Vector3 currentFrameMovement = Vector3.zero;
        currentFrameMovement = ism.movementAction.ReadValue<Vector2>().x * transform.right + ism.movementAction.ReadValue<Vector2>().y * transform.forward;
        currentFrameMovement = currentFrameMovement.normalized * power;
        force = currentFrameMovement - inputLastVerifiedUpdate;
        inputLastVerifiedUpdate = currentFrameMovement;
        ApplyForce(receiver, force);
    }

    public override void AddOrToggleReceiver(ForceReceiver r, bool togleMode)
    {
        throw new NotImplementedException();
    }
}
