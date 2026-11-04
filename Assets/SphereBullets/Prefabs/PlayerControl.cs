using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float distanceToSphere;
    public float speed;
    private float effectiveSpeed;
    public float rotationSpeed;
    private float effectiveRotSpeed;
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            effectiveSpeed = speed;
        else if (Input.GetKey(KeyCode.S))
            effectiveSpeed = -speed;
        else effectiveSpeed = 0f;
        
        if (Input.GetKey(KeyCode.A))
            effectiveRotSpeed = -rotationSpeed;
        else if (Input.GetKey(KeyCode.D))
            effectiveRotSpeed = rotationSpeed;
        else effectiveRotSpeed = 0f;
    }

    private void FixedUpdate()
    {
        RecalculateForward();
        ApplyMoveForward();
        ApplyRotation();
    }
    
    private void RecalculateForward()
    {
        Vector3 normal = transform.position.normalized;
        transform.position = normal * distanceToSphere;
        Vector3 dirToForward = Vector3.ProjectOnPlane(transform.forward, normal).normalized;
        if (dirToForward.sqrMagnitude <= 0.0001f) return;
        transform.rotation = Quaternion.LookRotation(dirToForward, normal);
    }
    
    protected void ApplyRotation()
    {
        if (rotationSpeed == 0) return;
        
        Vector3 normal = transform.position.normalized;
        Vector3 currentForward = Vector3.ProjectOnPlane(transform.forward, normal).normalized;
        Vector3 direction = Quaternion.AngleAxis(effectiveRotSpeed, normal) * currentForward;
        if (direction.sqrMagnitude > 0.0001f) transform.rotation = Quaternion.LookRotation(direction, normal);
    }
    
    protected void ApplyMoveForward()
    {
        rb.linearVelocity = transform.forward * effectiveSpeed;
    }
}
