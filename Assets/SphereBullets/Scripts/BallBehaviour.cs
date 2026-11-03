using System;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float kickForce;
    [SerializeField] private float bounceCoefficient;
    [SerializeField] private float frictionValue;
    private Vector3 sphereCenter = Vector3.zero;
    
    private Rigidbody rb;
    private Vector3 velocityBeforeCollision;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        velocityBeforeCollision = rb.linearVelocity;
        ApplyGravity();
    }

    #region Modificateurs de vitesse continus (gravité, friction)

    private void ApplyGravity()
    {
        Vector3 gravityDirection = (sphereCenter - rb.position).normalized;
        rb.AddForce(gravityDirection * gravity, ForceMode.Acceleration);
    }
    
    private void ApplyFriction()
    {
        if (rb.linearVelocity.magnitude <= 0.0001f)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }

        rb.linearVelocity -= rb.linearVelocity.normalized * frictionValue * Time.fixedDeltaTime;
    }

    #endregion

    #region Modificateurs de vitesse instantanés (collision avec projectile, rebond)

    private void InitiateCollisionWith(Collision other)
    {
        Rigidbody prb = other.gameObject.GetComponent<Rigidbody>();
            
        Vector3 launchDirection = (transform.position - prb.gameObject.transform.position).normalized;
        float launchSpeed = (prb.linearVelocity + launchDirection).magnitude * kickForce;
            
        rb.linearVelocity += launchDirection * launchSpeed;
    }
    
    private void ReboundOn(Collision other)
    {
        Vector3 normal = other.GetContact(0).normal.normalized;
        Vector3 incomingVelocity = velocityBeforeCollision;

        // Vérification de l'orientation de la normale
        if (Vector3.Dot(incomingVelocity, normal) > 0f) normal = -normal;
        float impactSpeed = -Vector3.Dot(incomingVelocity, normal);

        if (impactSpeed <= 0.01f) return;
            
        Vector3 reflectedVelocity = Vector3.Reflect(incomingVelocity, normal);
        rb.linearVelocity = reflectedVelocity * bounceCoefficient;
    }

    #endregion
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            InitiateCollisionWith(other);
        }
        if (other.gameObject.CompareTag("World"))
        {
            ReboundOn(other);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (rb.linearVelocity == Vector3.zero) return;
        if (other.gameObject.CompareTag("World"))
        {
            ApplyFriction();
        }
    }
}
