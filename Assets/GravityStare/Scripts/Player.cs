using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isGrounded;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
