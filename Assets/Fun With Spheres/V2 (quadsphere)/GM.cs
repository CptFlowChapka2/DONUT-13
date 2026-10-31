using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GM : MonoBehaviour
{
    #region singleton

    public static GM instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion
    
    private WorldRoot worldRoot;
    [SerializeField] private Vector3 testCoords;
    
    public Tile3D selectedTile;
    public Tile3D hoveredTile;

    public GameObject testPawn1;
    public GameObject testPawn2;
    
    public int currentTeamToPlay = 0;

    private void Start()
    {
        worldRoot = WorldRoot.instance;
        SpawnXPawnsPerPlayer(3);
        foreach (Tile3D tile in worldRoot.tiles.Values.ToList())
        {
            int rand = Random.Range(0, 4);
            switch (rand)
            {
                case 0:
                    tile.color = Color.aquamarine;
                    break;
                case 1:
                    tile.color = Color.blanchedAlmond;
                    break;
                case 2:
                    tile.color = Color.dodgerBlue;
                    break;
                case 3:
                    tile.color = Color.indianRed;
                    break;
            }
            tile.ChangeColor(tile.color);
        }
    }

    #region Débug visuel des Tiles

    public void DrawTiles(HashSet<Tile3D> tiles, bool draw = true)
    {
        foreach (Tile3D tile in tiles)
        {
            DrawTile(tile, draw);
        }
    }
    
    public void DrawTile(Tile3D tile, bool draw)
    {
        if (draw)
        {
            tile.meshRenderer.material.color = Color.blue;
        }
        else
        {
            tile.meshRenderer.material.color = tile.color;
        }
    }

    #endregion
    
    #region Management des pions

    private void SpawnXPawnsPerPlayer(int number)
    {
        List<Tile3D> availableTiles = worldRoot.tiles.Values.ToList();
        for (int i = 0; i < number * 2; i++)
        {
            int rand = Random.Range(0, availableTiles.Count);
            
            if (i < number)
            {
                SpawnPawnOnTile(testPawn1, availableTiles[rand]);
            }
            else
            {
                SpawnPawnOnTile(testPawn2, availableTiles[rand]);
            }
    
            availableTiles.Remove(availableTiles[rand]);
        }
    }
    
    public void SpawnPawnOnTile(GameObject prefab, Tile3D tile)
    {
        GameObject pawn = Instantiate(prefab, tile.center, Quaternion.identity);
        pawn.transform.up = tile.center.normalized;
        tile.pawnOnThisTile = pawn.GetComponent<Pawn>();
    }
    
    private void MovePawnToTile(Tile3D origin, Tile3D target)
    {
        target.pawnOnThisTile = origin.pawnOnThisTile;
        origin.pawnOnThisTile = null;
        Transform pawnTransform = target.pawnOnThisTile.gameObject.transform;
        pawnTransform.position = target.center;
        pawnTransform.up = target.center.normalized;
    }

    #endregion
    
    public void OnClick(Tile3D tile)
    {
        if (hoveredTile == null) return;

        HashSet<Tile3D> actedOnTiles = new HashSet<Tile3D>();
    
        if (selectedTile == null)
        {
            //Vérification que la case choisie contient bien un pion de l'équipe du joueur
            if (tile.pawnOnThisTile == null || tile.pawnOnThisTile.team != currentTeamToPlay) return;
            selectedTile = hoveredTile;
            actedOnTiles = worldRoot.GetReachableTilesByStraightOrthogonalMovement(selectedTile, 2);
            DrawTiles(actedOnTiles);
            return;
        }
        
        actedOnTiles = worldRoot.GetReachableTilesByStraightOrthogonalMovement(selectedTile, 2);
        
        if (selectedTile.pawnOnThisTile != null && selectedTile != tile && actedOnTiles.Contains(tile))
        {
            //Case pas dans la range
            if (tile.pawnOnThisTile == null && !actedOnTiles.Contains(tile)) return;
            
            DrawTiles(actedOnTiles, false);
            
            if (tile.pawnOnThisTile != null)
            {
                //Changement de pion sujet
                if (selectedTile.pawnOnThisTile.team == tile.pawnOnThisTile.team)
                {
                    selectedTile = hoveredTile;
                    DrawTiles(worldRoot.GetReachableTilesByStraightOrthogonalMovement(selectedTile, 2));
                    return;
                }
    
                //Capture de pion adverse
                Destroy(tile.pawnOnThisTile.gameObject);
            }
            
            //Déplacement du pion sur sa nouvelle case
            MovePawnToTile(selectedTile, tile);
            SwitchTurn();
        }
    }
    
    
    private void SwitchTurn()
    {
        selectedTile = null;
        currentTeamToPlay = 1 - currentTeamToPlay;
    }
}
