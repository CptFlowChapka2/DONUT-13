using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class ForceApplier : MonoBehaviour
{
    public float power;
    public Vector3 force;
    protected float influenceRadius;
    [SerializeField] protected Collider collider;

    protected void ApplyForce(ForceReceiver receiver, Vector3 force)
    {
        if (!receiver.forces.TryAdd(this, force)) //Ajoute une fois, puis ensuite ne fait que changer la value
        {
            receiver.forces[this] = force;
        }
    }

    public virtual void AddOrToggleReceiver(ForceReceiver r,bool togleMode){}
}
