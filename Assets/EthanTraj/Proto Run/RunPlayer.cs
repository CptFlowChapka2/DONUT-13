using System;
using UnityEngine;

public class RunPlayer : MonoBehaviour
{
    private Rigidbody rb;
    public float maxSpeed;
    public bool isRotating;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddForce(transform.forward, ForceMode.VelocityChange);
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }
}
