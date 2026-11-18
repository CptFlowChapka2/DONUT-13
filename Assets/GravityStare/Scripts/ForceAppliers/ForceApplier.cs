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
    [SerializeField] public Collider collider;
    public MeshRenderer meshRenderer;
    public Color activeColor = Color.greenYellow;
    public Color noColor = Color.antiqueWhite;

    public Color ActiveColor
    {
        get => activeColor;
        set
        {
            activeColor = value;
            if(receivers.Count>0)meshRenderer.material.SetColor("_BaseColor",activeColor);
        }
    }

    protected HashSet<ForceReceiver> receivers = new HashSet<ForceReceiver>();

    protected void ApplyForce(ForceReceiver receiver, Vector3 force)
    {
        if (!receiver.forces.TryAdd(this, force)) //Ajoute une fois, puis ensuite ne fait que changer la value
        {
            receiver.forces[this] = force;
        }
    }

    public abstract void CreateAndApplyForce(ForceReceiver receiver);

    public abstract void AddOrToggleReceiver(ForceReceiver r, bool togleMode);
    
}
