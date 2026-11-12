using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class ForceApplier : MonoBehaviour
{
    public float power;
    public Vector3 force;
    protected float influenceRadius;
    [SerializeField] protected Collider collider;
    
    protected HashSet<ForceReceiver> receivers = new HashSet<ForceReceiver>();

    protected void ApplyForce(ForceReceiver receiver, Vector3 force)
    {
        if (!receiver.forces.TryAdd(this, force)) //Ajoute une fois, puis ensuite ne fait que changer la value
        {
            receiver.forces[this] = force;
        }
    }

    public abstract void CreateAndApllyForce(ForceReceiver receiver);

    public virtual void AddOrToggleReceiver(ForceReceiver r,bool togleMode){}
}
