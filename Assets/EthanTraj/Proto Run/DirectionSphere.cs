using System;
using Unity.VisualScripting;
using UnityEngine;

public class DirectionSphere : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed;
    public Transform dir;
    
    void Update()
    {
        transform.RotateAround(player.position, transform.up, rotationSpeed * Time.deltaTime);
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            player.LookAt(dir.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cell"))
        {
            // Debug.Log(other.gameObject.name);
            dir = other.transform;
        }
    }
}
