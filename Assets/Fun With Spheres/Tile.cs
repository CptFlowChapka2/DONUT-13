using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Latitude latitude;
    public Vector2 coords = new Vector2(0, 0);
    public Color color;
    public MeshRenderer meshRenderer;
    private Vector3 center;
    public bool isSingularity = false;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        center = GetComponent<MeshCollider>().bounds.center;
        meshRenderer.material.color = color;

        if (!isSingularity) return;
        meshRenderer.enabled = false;
    }

    // private void OnMouseEnter()
    // {
    //     if (isSingularity)
    //     {
    //         meshRenderer.enabled = true;
    //     }
    //     meshRenderer.material.color = Color.blue;
    // }
    //
    // private void OnMouseExit()
    // {
    //     if (isSingularity)
    //     {
    //         meshRenderer.enabled = false;
    //     }
    //     meshRenderer.material.color = color;
    // }
}

public struct RelativeTile
{
    public Tile tile;
    public int orthogonalDistance;
}