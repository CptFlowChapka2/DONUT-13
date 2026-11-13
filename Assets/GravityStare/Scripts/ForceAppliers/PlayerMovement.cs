using System;
using UnityEngine;

public class PlayerMovement : ForceApplier
{
    [SerializeField] private Player player;
    [SerializeField] private ForceReceiver receiver;

    private InputSystemManager ism;
    private Vector2 inputLastVerifiedUpdate = new Vector2(0, 0);

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
        force = Vector3.zero;

        //On applique aucune force si l'on est dans les airs où que la force a déjà été appliquée
        if (!player.isGrounded || ism.movementAction.ReadValue<Vector2>() == inputLastVerifiedUpdate)
        {
            ApplyForce(receiver, force);
            return;
        }
        
        //On applique une force une seule fois, quand la value de l'input change
        //Pour ce faire, on annule les anciens mouvements du player (après tout il est grounded donc ça change rien);
        //Puis on applique une force dans le sens de l'input par rapport au forward.
        receiver.rb.linearVelocity = Vector3.zero;
        force = new Vector3(Input.GetAxis("Horizontal") * transform.forward.x, 0, Input.GetAxis("Vertical") * transform.forward.z) * power;
        inputLastVerifiedUpdate = ism.movementAction.ReadValue<Vector2>();
        ApplyForce(receiver, force);
    }
}
