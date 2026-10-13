using System;
using UnityEngine;
using UnityEngine.Events;

public class ColiderScript : MonoBehaviour
{
    public UnityEvent<int,Collider> colliderTriggerEvent = new UnityEvent<int,Collider>();

    private void OnTriggerExit(Collider other)
    {
        colliderTriggerEvent.Invoke(2,other);
    }

    private void OnTriggerEnter(Collider other)
    {
        colliderTriggerEvent.Invoke(0,other);

    }

    private void OnTriggerStay(Collider other)
    {
        colliderTriggerEvent.Invoke(1,other);

    }
}
