using System;
using System.Collections.Generic;
using UnityEngine;

public class CellBhv : MonoBehaviour
{
    public Rigidbody rb;
    private LineRenderer trail;
    public float maxSpeed;
    public List<Transform> neighbors;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        CreateLink();
    }

    void FixedUpdate()
    {
        rb.AddForce(transform.forward, ForceMode.VelocityChange);
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }

    void CreateLink()
    {
        if (neighbors.Count == 0)
            trail.enabled = false;
        
        else
        {
            trail.enabled = true;
            foreach (Transform n in neighbors)
            {
                trail.SetPosition(0, transform.position);
                trail.SetPosition(1, n.position);
                float dist = Vector3.Distance(transform.position, n.position) / 2f;

                if (dist >= 0.5f)
                {
                    rb.AddForce((n.position - transform.position).normalized * (dist / 5f), ForceMode.VelocityChange);
                    transform.Rotate(transform.up, dist * Time.deltaTime * 25f);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cell"))
        {
            neighbors.Add(other.transform);
            Debug.Log(neighbors[0].transform.gameObject.name);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cell"))
        {
            neighbors.Remove(other.transform);
            Debug.Log(neighbors[0].transform.gameObject.name);
        }
    }
}
