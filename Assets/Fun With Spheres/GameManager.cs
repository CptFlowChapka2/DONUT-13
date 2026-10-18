using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WorldGenerator worldGenerator;
    [SerializeField] private Vector2 testCoords;

    private void Start()
    {
        HashSet<RelativeTile> tiles = new HashSet<RelativeTile>();
        HashSet<RelativeTile> tiles2 = new HashSet<RelativeTile>();
        
        tiles = GetTilesOnLatitude(testCoords);
        foreach (RelativeTile tile in tiles)
        {
            tile.tile.meshRenderer.material.color = new Color(((float)tile.orthogonalDistance/10f), ((float)tile.orthogonalDistance/10f), 1, 1);
            if (!tile.tile.isSingularity) continue;
            tile.tile.meshRenderer.enabled = true;
        }
        
        tiles2 = GetTilesOnLongitude(testCoords);
        foreach (RelativeTile tile in tiles2)
        {
            tile.tile.meshRenderer.material.color = new Color(((float)tile.orthogonalDistance/10f), ((float)tile.orthogonalDistance/10f), 1, 1);
            if (!tile.tile.isSingularity) continue;
            tile.tile.meshRenderer.enabled = true;
        }
    }

    public HashSet<RelativeTile> GetTilesOnLatitude(Vector2 coordinates)
    {
        HashSet<RelativeTile> concernedTiles = new HashSet<RelativeTile>();
        foreach (var pair in worldGenerator.tiles)
        {
            if ((int)pair.Key.y % 8 != (int)coordinates.y % 8) continue;
            RelativeTile rTiles = new RelativeTile();
            rTiles.tile = pair.Value;
            rTiles.orthogonalDistance = GetOrthogonalDistanceToTile(testCoords, pair.Value);
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
            rTiles.orthogonalDistance = GetOrthogonalDistanceToTile(testCoords, pair.Value);
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
}
