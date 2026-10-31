using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile3D : MonoBehaviour
{
    #region Setup
    
    private WorldRoot world;
    private GM gameManager;
    public Vector3Int coords = new Vector3Int(0, 0, 0);
    public Color color = Color.white;
    public MeshRenderer meshRenderer;
    public MeshCollider meshCollider;
    public Vector3 center;

    #endregion

    public Pawn pawnOnThisTile;
    public bool isSelectable = true;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
        center = meshCollider.bounds.center;
    }

    private void Start()
    {
        world = WorldRoot.instance;
        gameManager = GM.instance;
    }

    private void HoverColoring(bool on = true)
    {
        gameManager.hoveredTile = this;
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
        gameManager.OnClick(this);
        //WorldRoot.instance.TestOrthoDraw(this);
    }
}