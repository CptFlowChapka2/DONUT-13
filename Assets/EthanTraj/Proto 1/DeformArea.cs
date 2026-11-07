using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeformArea : MonoBehaviour
{
    // public Vector3 deformStreght;
    public float deformStreght;

    private void Start()
    {
        // deformStreght.x = -Mathf.Floor(transform.position.x);
        // deformStreght.y = -Mathf.Floor(transform.position.y);
        // deformStreght.z = -Mathf.Floor(transform.position.z);
        // deformStreght = Random.Range(90f, 180f);
        // deformStreght = Random.Range(90f, 180f);
        int test = Random.Range(0, 2);
        if (test == 0)
            deformStreght = deformStreght;
        else
            deformStreght = -deformStreght;
    }
}
