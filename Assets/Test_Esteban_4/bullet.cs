using System;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;

    private void FixedUpdate()
    {
        transform.forward = rb.linearVelocity;
        rb.AddForce(transform.forward*speed,ForceMode.VelocityChange);
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, speed);
    }
}
