using System;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class Projectile : MonoBehaviour
{
    public Camera[] personalCamera = new Camera[2];
    public Rigidbody rb;
    public float distanceToSphere;
    public ProjectileData projectileData;

    private float speed;
    [NonSerialized] public float rotationSpeed;
    private float heightBounciness;
    private float lengthBounciness;

    public float maxSpeed;
    public float dashingLength;

    public bool isControlled = false;
    public bool dashing = false;
    
    protected float tBounciness;
    protected float tDashing;

    private bool dashBufferInput = false;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = projectileData.speed;
        rotationSpeed = projectileData.rotationSpeed;
        heightBounciness = projectileData.heightBounciness;
        lengthBounciness = projectileData.lengthBounciness;
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) dashBufferInput = true;
    }
    
    protected virtual void FixedUpdate()
    {
        RecalculateForward();
        ApplyMoveForward();
        ApplyRotation();
        ApplyBounciness();
        ApplyControls();
    }

    private void ApplyControls()
    {
        if (!isControlled) return;

        if (dashBufferInput)
        {
            dashBufferInput = false;
            if (!dashing)
            {
                speed = maxSpeed;
                dashing = true;
                tDashing = dashingLength;
            }
        }

        if (dashing)
        {
            tDashing -= Time.fixedDeltaTime;
            if (tDashing <= 0)
            {
                dashing = false;
                speed = projectileData.speed;
            }
        }
    }

    private void RecalculateForward()
    {
        Vector3 normal = transform.position.normalized;
        transform.position = normal * distanceToSphere;
        Vector3 dirToForward = Vector3.ProjectOnPlane(transform.forward, normal).normalized;
        if (dirToForward.sqrMagnitude <= 0.0001f) return;
        transform.rotation = Quaternion.LookRotation(dirToForward, normal);
    }

    protected void ApplyBounciness()
    {
        if (heightBounciness <= 0 || lengthBounciness <= 0) return;
        
        tBounciness += Time.fixedDeltaTime;
        if (tBounciness >= lengthBounciness) tBounciness = 0;
        distanceToSphere = 5 + ((4 * heightBounciness) / (lengthBounciness * lengthBounciness)) * tBounciness *
            (lengthBounciness - tBounciness);
    }

    protected void ApplyRotation()
    {
        if (rotationSpeed == 0) return;
        
        Vector3 normal = transform.position.normalized;
        Vector3 currentForward = Vector3.ProjectOnPlane(transform.forward, normal).normalized;
        Vector3 direction = Quaternion.AngleAxis(rotationSpeed, normal) * currentForward;
        if (direction.sqrMagnitude > 0.0001f) transform.rotation = Quaternion.LookRotation(direction, normal);
    }
    
    protected void ApplyMoveForward()
    {
        rb.linearVelocity = transform.forward * speed;
    }
}
