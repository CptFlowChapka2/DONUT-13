using System;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    [SerializeField] private float gravity;
    [SerializeField] private float kickForce;
    [SerializeField] private float frictionValue;
    private float kickLengthBeforeStopping;
    private Rigidbody rb;
    private float t;
    
    private Vector3 kickVector = Vector3.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 gravityVector = gravity * (-transform.position.normalized);
        rb.linearVelocity = kickVector + gravityVector;

        if (kickVector.magnitude == 0) return;
        t -= Time.fixedDeltaTime;
        if (t <= 0)
        {
            t = 0;
        }
        kickVector *= t / kickLengthBeforeStopping;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            kickVector = (kickVector.normalized + (transform.position - other.gameObject.transform.position).normalized).normalized * kickForce * other.linearVelocity.magnitude;
            kickLengthBeforeStopping = kickVector.magnitude / frictionValue;
            t = kickLengthBeforeStopping;
        }
    }
}
