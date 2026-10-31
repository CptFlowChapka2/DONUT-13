using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WorldGenerator worldGenerator;
    [SerializeField] private Vector2 testCoords;
    
    public Tile selectedTile;
    public Tile hoveredTile;

    public GameObject testPawn1;
    public GameObject testPawn2;
    
    public int currentTeamToPlay = 0;

    private void Start()
    {
        SpawnXPawnsPerPlayer(3);
        foreach (Tile tile in worldGenerator.tiles.Values)
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

    public void DrawTiles(HashSet<Tile> tiles, bool draw = true)
    {
        foreach (Tile tile in tiles)
        {
            DrawTile(tile, draw);
        }
    }

    public void DrawTile(Tile tile, bool draw)
    {
        if (draw)
        {
            tile.meshRenderer.material.color = Color.blue;
            if (!tile.isSingularity) return;
            tile.meshRenderer.material.color = Color.red;
            tile.meshRenderer.enabled = true;
        }
        else
        {
            tile.meshRenderer.material.color = tile.color;
            if (!tile.isSingularity) return;
            tile.meshRenderer.enabled = false;
        }
    }

    #endregion

    #region Récupération de Tiles selon une coordonnée précise

    public HashSet<Tile> GetNearbyTiles(Vector2 coordinates, int range = 10)
    {
        HashSet<Tile> toReturn = new HashSet<Tile>();
        HashSet<Tile> tiles = GetTilesOnLatitude(coordinates);
        HashSet<Tile> tiles2 = GetTilesOnLongitude(coordinates);
        
        foreach (Tile tile in tiles)
        {
            if (GetOrthogonalDistanceToTile(coordinates, tile) <= range) toReturn.Add(tile);
        }
        
        foreach (Tile tile in tiles2)
        {
            if (GetOrthogonalDistanceToTile(coordinates, tile) <= range) toReturn.Add(tile);
        }
        
        return toReturn;
    }
    
    public HashSet<Tile> GetTilesOnLatitude(Vector2 coordinates)
    {
        HashSet<Tile> concernedTiles = new HashSet<Tile>();
        foreach (var pair in worldGenerator.tiles)
        {
            if ((int)pair.Key.y % 8 != (int)coordinates.y % 8) continue;
            concernedTiles.Add(pair.Value);
        }
        return concernedTiles;
    }
    
    public HashSet<Tile> GetTilesOnLongitude(Vector2 coordinates)
    {
        HashSet<Tile> concernedTiles = new HashSet<Tile>();
        foreach (var pair in worldGenerator.tiles)
        {
            if ((int)pair.Key.x != (int)coordinates.x) continue;
            concernedTiles.Add(pair.Value);
        }
        return concernedTiles;
    }

    public int GetOrthogonalDistanceToTile(Vector2 origin, Tile target)
    {
        if ((int)origin.y == (int)target.coords.y) return Mathf.Abs((int)target.coords.x - (int)origin.x);

        if ((int)origin.y % 8 == (int)target.coords.y % 8)
        {
            int relativeX = (int)origin.x * 2 + ((int)target.coords.x - (int)origin.x);
            return 9 - Mathf.Abs(relativeX - 9);
        }
            
        if ((int)origin.x == (int)target.coords.x)
        {
            int relativeY = Mathf.Abs((int)target.coords.y - (int)origin.y);
            return 8 - Mathf.Abs(relativeY - 8);
        }
            
        Debug.Log("Invalid Tile! It's not orthogonally connected to the studied tile!");
        return -1;
    }
    
    #endregion
    
    #region Management des pions

    private void SpawnXPawnsPerPlayer(int number)
    {
        List<Tile> availableTiles = new List<Tile>();
        foreach (Tile tile in worldGenerator.tiles.Values)
        {
            if (!tile.isSingularity)
            {
                availableTiles.Add(tile);
            }
        }
        
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
    
    public void SpawnPawnOnTile(GameObject prefab, Tile tile)
    {
        GameObject pawn = Instantiate(prefab, tile.center, Quaternion.identity);
        pawn.transform.up = tile.center.normalized;
        tile.pawnOnThisTile = pawn.GetComponent<Pawn>();
    }
    
    private void MovePawnToTile(Tile origin, Tile target)
    {
        target.pawnOnThisTile = origin.pawnOnThisTile;
        origin.pawnOnThisTile = null;
        Transform pawnTransform = target.pawnOnThisTile.gameObject.transform;
        pawnTransform.position = target.center;
        pawnTransform.up = target.center.normalized;
    }

    #endregion
    
    public void OnClick(Tile tile)
    {
        if (hoveredTile == null) return;

        if (selectedTile == null)
        {
            //Vérification que la case choisie contient bien un pion de l'équipe du joueur
            if (tile.pawnOnThisTile == null || tile.pawnOnThisTile.team != currentTeamToPlay) return;
            selectedTile = hoveredTile;
            DrawTiles(GetNearbyTiles(selectedTile.coords, 2));
            return;
        }
        
        HashSet<Tile> concernedTiles = GetNearbyTiles(selectedTile.coords, 2);
        
        if (selectedTile.pawnOnThisTile != null && selectedTile != tile && concernedTiles.Contains(tile))
        {
            //Case pas dans la range
            if (tile.pawnOnThisTile == null && !concernedTiles.Contains(tile)) return;
            
            DrawTiles(concernedTiles, false);
            
            if (tile.pawnOnThisTile != null)
            {
                //Changement de pion sujet
                if (selectedTile.pawnOnThisTile.team == tile.pawnOnThisTile.team)
                {
                    selectedTile = hoveredTile;
                    DrawTiles(GetNearbyTiles(selectedTile.coords, 2));
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
