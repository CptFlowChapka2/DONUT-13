using System;
using UnityEngine;

namespace GravityStare.Scripts.ForceAppliers
{
    public class PlayerJetpack :ForceApplier
    {
        private ForceReceiver ownReceiver;
        

        private void Start()
        {
            ownReceiver = Player.instance.thisForceReceiver;
            ownReceiver.forces.TryAdd(this, Vector3.zero);
        }

        private void FixedUpdate()
        {
            CreateAndApplyForce(ownReceiver);
        }

        public override void CreateAndApplyForce(ForceReceiver receiver)
        {
            if (Input.GetKey(KeyCode.Space) && !Player.instance.isGrounded)
            {
                force = Player.instance.camera.transform.forward.normalized * (Time.fixedDeltaTime * power);
            }
            else
            {
                force = Vector3.zero;
            }
            ApplyForce(receiver,force);
        }

        public override void AddOrToggleReceiver(ForceReceiver r, bool togleMode)
        {
            throw new System.NotImplementedException();
        }
    }
}