using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    #region Setup

    public Latitude latitude;
    public Vector2 coords = new Vector2(0, 0);
    public Color color;
    public MeshRenderer meshRenderer;
    public Vector3 center;
    public bool isSingularity = false;

    #endregion

    public Pawn pawnOnThisTile;
    public bool isSelectable = true;
    
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        center = GetComponent<MeshCollider>().bounds.center;

        if (!isSingularity) return;
        isSelectable = false;
        meshRenderer.enabled = false;
    }

    private void HoverColoring(bool on = true)
    {
        latitude.worldGenerator.gameManager.hoveredTile = this;
        Color meshColor = new Color();
        if (on) meshColor = Color.black;
        else meshColor = color;
        ChangeColor(meshColor);
    }

    public void ChangeColor(Color color)
    {
        meshRenderer.material.color = color;
    }
    
    private void OnMouseEnter()
    {
        if (!isSelectable) return;
        HoverColoring();
    }

    private void OnMouseExit()
    {
        if (!isSelectable) return;
        clicking = false;
        HoverColoring(false);
    }

    private bool clicking = false;
    private void OnMouseDown()
    {
        if (!isSelectable) return;
        clicking = true;
    }

    private void OnMouseUp()
    {
        if (!clicking || !isSelectable) return;
        latitude.worldGenerator.gameManager.OnClick(this);
    }
}