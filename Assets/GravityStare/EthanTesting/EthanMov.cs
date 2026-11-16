using System;
using UnityEngine;

public class EthanMov : MonoBehaviour
{
    public Rigidbody rb;
    public Vector2 input;
    public Vector3 movementDir;
    public float maxSpeed;
    public float gravity;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.queriesHitBackfaces = false;
    }

    
    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input = input.normalized;
        movementDir = (transform.forward * input.y) + (transform.right * input.x);
        movementDir = movementDir.normalized;
    }

    private void FixedUpdate()
    {
        rb.AddForce(movementDir, ForceMode.VelocityChange);
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);
        rb.AddForce(-transform.up * gravity);
        
        if (Physics.SphereCast(transform.position,1f, -transform.up, out RaycastHit hit, 2f))
            transform.up = hit.normal;
    }
}
