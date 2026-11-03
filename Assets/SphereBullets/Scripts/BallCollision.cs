using System;
using UnityEngine;
using UnityEngine.Events;

public class BallCollision : MonoBehaviour
{
    public UnityEvent<Collision> onCollision;

    private void OnCollisionEnter(Collision other)
    {
        onCollision.Invoke(other);
    }
}
