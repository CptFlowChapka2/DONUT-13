using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerControler_E : MonoBehaviour
{
    private float activeSpeed;
    public float groundSpeed;
    public float airSpeed;
    public float rotSpeed;
    public float jumpForce;
    
    private Vector2 inputDir;
    private Vector3 movementDir;
    private Vector3 appliedMovement;
    
    private float mouseX;
    private float mouseY;
    
    private float xRot;
    private float yRot;
    
    public Transform mesh;
    public Camera cam;
    private Rigidbody rb;

    public bool isGrounded;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        Physics.queriesHitBackfaces = false;
    }

    void Update()
    {
        GetHeadMovement();
        GetMovementDir();

        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f))
        {
            if (hit.collider.gameObject.CompareTag("Ground"))
                isGrounded = true;
        }
        
        else isGrounded = false;

        if (isGrounded)
        {
            rb.linearDamping = 7f;
            activeSpeed = groundSpeed;
            
        }
        
        else
        {
            rb.linearDamping = 0f;
            activeSpeed = airSpeed;
        }
        
        rb.AddForce(movementDir.normalized * (activeSpeed * 10f), ForceMode.Force);
    }

    void GetMovementDir()
    {
        inputDir.x = Input.GetAxisRaw("Horizontal");
        inputDir.y = Input.GetAxisRaw("Vertical");

        movementDir = (transform.right * inputDir.x) + (transform.forward * inputDir.y);
    }

    void GetHeadMovement()
    {
        mouseX = Input.GetAxis("Mouse X") * (Time.deltaTime * rotSpeed);
        mouseY = Input.GetAxis("Mouse Y") * (Time.deltaTime * rotSpeed);
        
        yRot += mouseX;
        xRot -= mouseY;

        xRot = Mathf.Clamp(xRot, -60f, 60f);

        transform.rotation = Quaternion.Euler(0f, yRot, 0f);
        cam.transform.rotation = Quaternion.Euler(xRot, yRot, 0f);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
