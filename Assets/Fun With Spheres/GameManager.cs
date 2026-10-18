using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WorldGenerator worldGenerator;
    [SerializeField] private Vector2 testCoords;
    
    public Tile selectedTile;
    public Tile hoveredTile;

    #region Débug visuel des cases proches

    public void DrawNearbyTiles(Vector2 coords, bool draw = true)
    {
        HashSet<RelativeTile> tiles = GetTilesOnLatitude(coords);
        HashSet<RelativeTile> tiles2 = GetTilesOnLongitude(coords);
        
        foreach (RelativeTile rTile in tiles)
        {
            DrawTile(draw, rTile);
        }
        
        foreach (RelativeTile rTile in tiles2)
        {
            DrawTile(draw, rTile);
        }
    }

    public void DrawTile(bool draw, RelativeTile rTile)
    {
        if (draw)
        {
            rTile.tile.meshRenderer.material.color = new Color(((float)rTile.orthogonalDistance/10f), ((float)rTile.orthogonalDistance/10f), 1, 1);
            if (!rTile.tile.isSingularity) return;
            rTile.tile.meshRenderer.enabled = true;
        }
        else
        {
            rTile.tile.meshRenderer.material.color = Color.white;
            if (!rTile.tile.isSingularity) return;
            rTile.tile.meshRenderer.enabled = false;
        }
    }

    #endregion

    public HashSet<RelativeTile> GetTilesOnLatitude(Vector2 coordinates)
    {
        HashSet<RelativeTile> concernedTiles = new HashSet<RelativeTile>();
        foreach (var pair in worldGenerator.tiles)
        {
            if ((int)pair.Key.y % 8 != (int)coordinates.y % 8) continue;
            RelativeTile rTiles = new RelativeTile();
            rTiles.tile = pair.Value;
            rTiles.orthogonalDistance = GetOrthogonalDistanceToTile(coordinates, pair.Value);
            concernedTiles.Add(rTiles);
        }
        return concernedTiles;
    }
    
    public HashSet<RelativeTile> GetTilesOnLongitude(Vector2 coordinates)
    {
        HashSet<RelativeTile> concernedTiles = new HashSet<RelativeTile>();
        foreach (var pair in worldGenerator.tiles)
        {
            if ((int)pair.Key.x != (int)coordinates.x) continue;
            RelativeTile rTiles = new RelativeTile();
            rTiles.tile = pair.Value;
            rTiles.orthogonalDistance = GetOrthogonalDistanceToTile(coordinates, pair.Value);
            concernedTiles.Add(rTiles);
        }
        return concernedTiles;
    }

    public int GetOrthogonalDistanceToTile(Vector2 origin, Tile target)
    {
        if ((int)origin.y == (int)target.coords.y) return Mathf.Abs((int)target.coords.x - (int)origin.x);

        if ((int)origin.y % 8 == (int)target.coords.y % 8)
        {
            int relativeX = (int)origin.x * 2 + ((int)target.coords.x - (int)origin.x);
            return 10 - Mathf.Abs(relativeX - 10);
        }
            
        if ((int)origin.x == (int)target.coords.x)
        {
            int relativeY = Mathf.Abs((int)target.coords.y - (int)origin.y);
            return 8 - Mathf.Abs(relativeY - 8);
        }
            
        Debug.Log("Invalid Tile! It's not orthogonally connected to the studied tile!");
        return -1;
    }

    public void OnClick()
    {
        if (hoveredTile == null) return;
        if (selectedTile != null)
        {
            DrawNearbyTiles(selectedTile.coords, false);
        }

        selectedTile = hoveredTile;
        DrawNearbyTiles(selectedTile.coords);
    }
}
