using System;
using UnityEngine;

public class CaseScript : MonoBehaviour
{
    public MeshRenderer mr;
    public Gradient gradient;
    public float value = 0.5f;
    public float maxPerdition = 0.15f;
    public float heighMultiplier = 10;

    private void Start()
    {
        SetValue(value);
    }

    public void SetValue(float input)
    {
        value = input;
        transform.position= new Vector3(transform.position.x,(value*heighMultiplier),transform.position.z);
        mr.material.color = gradient.Evaluate(value);
    }

}
