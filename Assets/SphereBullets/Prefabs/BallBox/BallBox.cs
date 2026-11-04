using System;
using UnityEngine;

public class BallBox : MonoBehaviour
{
    public GameObject ballToCreate;
    public int numberOfBalls;
    public ProjectileManager spawner;
    
    private void OnTriggerEnter(Collider other)
    {
        spawner.MultiInstantiateAndInitialize(ballToCreate, numberOfBalls, transform.position, transform.forward);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
