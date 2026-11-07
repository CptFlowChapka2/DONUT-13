using System;
using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    private Rigidbody rb;
    public float accel;
    public float maxSpeed;
    private float t;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (t >= 0f) t -= Time.deltaTime;
    }


    void FixedUpdate()
    {
        rb.AddForce(transform.forward * accel, ForceMode.Acceleration);
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeformTrigger") && t <= 0f)
        {
            // rb.linearVelocity *= 0.25f;
            rb.linearVelocity = Vector3.zero;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DeformTrigger"))
        {
            t = 5f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("DeformTrigger"))
        {
            // transform.forward += other.GetComponent<DeformArea>().deformStreght * Time.deltaTime;
            // transform.Rotate(transform.up, other.GetComponent<DeformArea>().deformStreght * Time.deltaTime);
            transform.RotateAround(other.transform.position, other.transform.up, other.GetComponent<DeformArea>().deformStreght * Time.deltaTime);
        }
    }
}
