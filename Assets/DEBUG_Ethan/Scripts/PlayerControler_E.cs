using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerControler_E : MonoBehaviour
{
    public float speed;
    public float rotSpeed;
    public Vector2 inputDir;
    public Vector3 movementDir;
    public Transform mesh;
    public Camera cam;
    
    public float mouseX;
    public float xRot;
    
    public float mouseY;
    public float yRot;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cam = Camera.main;
    }

    void Update()
    {
        GetHeadMovement();
        GetMovementDir();
        
        if (inputDir != Vector2.zero)
            transform.position += movementDir.normalized * (Time.deltaTime * speed);
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
        yRot += mouseX;
        
        mouseY = Input.GetAxis("Mouse Y") * (Time.deltaTime * rotSpeed);
        xRot -= mouseY;

        transform.rotation = Quaternion.Euler(0f, yRot, 0f);
        cam.transform.rotation = Quaternion.Euler(xRot, yRot, 0f);
    }
}
