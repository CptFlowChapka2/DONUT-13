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
}
