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

    private void OnMouseEnter()
    {
        if (isSingularity) return;
        latitude.worldGenerator.gameManager.hoveredTile = this;
        Color meshColor = meshRenderer.material.color;
        meshColor = new Color(meshColor.r, meshColor.g, 0, meshColor.a);
        meshRenderer.material.color = meshColor;
    }
    
    private void OnMouseExit()
    {
        if (isSingularity) return;
        clicking = false;
        latitude.worldGenerator.gameManager.hoveredTile = null;
        Color meshColor = meshRenderer.material.color;
        meshColor = new Color(meshColor.r, meshColor.g, 1, meshColor.a);
        meshRenderer.material.color = meshColor;
    }

    private bool clicking = false;
    private void OnMouseDown()
    {
        clicking = true;
    }

    private void OnMouseUp()
    {
        if (!clicking) return;
        latitude.worldGenerator.gameManager.OnClick();
    }
}

public struct RelativeTile
{
    public Tile tile;
    public int orthogonalDistance;
}