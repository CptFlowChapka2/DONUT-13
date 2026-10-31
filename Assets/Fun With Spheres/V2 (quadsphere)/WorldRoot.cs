using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldRoot : MonoBehaviour
{
    #region singleton

    public static WorldRoot instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion
    
    [SerializeField] public Dictionary<Vector3Int, Tile3D> tiles;
    [SerializeField] private int maxDistancePossible;

    public HashSet<Tile3D> GetTilesOnAxis(Tile3D tile, Axis axis)
    {
        switch (axis)
        {
            case Axis.X:
                return tiles.Values.Where(x => x.coords.x == tile.coords.x).ToHashSet();
            case Axis.Y:
                return tiles.Values.Where(x => x.coords.y == tile.coords.y).ToHashSet();
            case Axis.Z:
                return tiles.Values.Where(x => x.coords.z == tile.coords.z).ToHashSet();
            default:
                return null;
        }
    }

    public HashSet<Tile3D> GetReachableTilesByStraightOrthogonalMovement(Tile3D tile, int reach = 20)
    {
        HashSet<Tile3D> studiedTiles = new HashSet<Tile3D>();
        if (GetOwnFace(tile).axis != Axis.X) studiedTiles.UnionWith(GetTilesOnAxis(tile, Axis.X));
        if (GetOwnFace(tile).axis != Axis.Y) studiedTiles.UnionWith(GetTilesOnAxis(tile, Axis.Y));
        if (GetOwnFace(tile).axis != Axis.Z) studiedTiles.UnionWith(GetTilesOnAxis(tile, Axis.Z));
        HashSet<Tile3D> reachableTiles = studiedTiles.Where(x => GetOrthogonalDistanceToTile(tile, x) <= reach).ToHashSet();
        return reachableTiles;
    }

    public Tile3D GetNearestTileOnDirection(Tile3D tile, Vector3Int direction)
    {
        Vector3Int coords = tile.coords;
        switch (GetOwnFace(tile).axis)
        {
            case Axis.X:
                if (direction == new Vector3Int(1, 0, 0) || direction == new Vector3Int(-1, 0, 0)) return null;
                coords += direction;
                if (!tiles.ContainsKey(coords))
                {
                    if (GetOwnFace(tile).orientation == false) coords += new Vector3Int(1, 0, 0);
                    else coords += new Vector3Int(-1, 0, 0);
                }
                return tiles[coords];
                
            case Axis.Y:
                if (direction == new Vector3Int(0, 1, 0) || direction == new Vector3Int(0, -1, 0)) return null;
                coords += direction;
                if (!tiles.ContainsKey(coords))
                {
                    if (GetOwnFace(tile).orientation == false) coords += new Vector3Int(0, 1, 0);
                    else coords += new Vector3Int(0, -1, 0);
                }
                return tiles[coords];
                
            case Axis.Z:
                if (direction == new Vector3Int(0, 0, 1) || direction == new Vector3Int(0, 0, -1)) return null;
                coords += direction;
                if (!tiles.ContainsKey(coords))
                {
                    if (GetOwnFace(tile).orientation == false) coords += new Vector3Int(0, 0, 1);
                    else coords += new Vector3Int(0, 0, -1);
                }
                return tiles[coords];
            
            default:
                return null;
        }
    }
    
    public HashSet<Tile3D> GetAllNeighbours(Tile3D tile)
    {
        HashSet<Tile3D> neighbours = new HashSet<Tile3D>();
        Tile3D neighbour = GetNearestTileOnDirection(tile, new Vector3Int(1, 0, 0));
        if (neighbour != null) neighbours.Add(neighbour);
        neighbour = GetNearestTileOnDirection(tile, new Vector3Int(-1, 0, 0));
        if (neighbour != null) neighbours.Add(neighbour);
        neighbour = GetNearestTileOnDirection(tile, new Vector3Int(0, 1, 0));
        if (neighbour != null) neighbours.Add(neighbour);
        neighbour = GetNearestTileOnDirection(tile, new Vector3Int(0, -1, 0));
        if (neighbour != null) neighbours.Add(neighbour);
        neighbour = GetNearestTileOnDirection(tile, new Vector3Int(0, 0, 1));
        if (neighbour != null) neighbours.Add(neighbour);
        neighbour = GetNearestTileOnDirection(tile, new Vector3Int(0, 0, -1));
        if (neighbour != null) neighbours.Add(neighbour);
        return neighbours;
    }

    public Face GetOwnFace(Tile3D tile)
    {
        Face toReturn = new Face();
        if (tile.coords.x == 0)
        {
            toReturn.axis = Axis.X;
            toReturn.orientation = false;
        }
        else if (tile.coords.x == 5)
        {
            toReturn.axis = Axis.X;
            toReturn.orientation = true;
        }
        else if (tile.coords.y == 0)
        {
            toReturn.axis = Axis.Y;
            toReturn.orientation = false;
        }
        else if (tile.coords.y == 5)
        {
            toReturn.axis = Axis.Y;
            toReturn.orientation = true;
        }
        else if (tile.coords.z == 0)
        {
            toReturn.axis = Axis.Z;
            toReturn.orientation = false;
        }
        else if (tile.coords.z == 5)
        {
            toReturn.axis = Axis.Z;
            toReturn.orientation = true;
        }

        return toReturn;
    }

    public int GetOrthogonalDistanceToTile(Tile3D origin, Tile3D target)
    {
        if (origin == target) return 0; 
        
        HashSet<Tile3D> alreadyStudiedTiles = new HashSet<Tile3D>();
        HashSet<Tile3D> currentlyStudiedTiles = new HashSet<Tile3D> { origin };
        HashSet<Tile3D> nextToBeStudiedTiles = new HashSet<Tile3D>();
        
        for (int i = 1; i <= maxDistancePossible; i++)
        {
            foreach (Tile3D tile in currentlyStudiedTiles)
            {
                alreadyStudiedTiles.Add(tile);
                HashSet<Tile3D> neighbours = GetAllNeighbours(tile);
                neighbours = neighbours.Where(x => !alreadyStudiedTiles.Contains(x) && !currentlyStudiedTiles.Contains(x)).ToHashSet();
                if (neighbours.Contains(target)) return i;
                nextToBeStudiedTiles.UnionWith(neighbours);
            }
            currentlyStudiedTiles.Clear();
            currentlyStudiedTiles.UnionWith(nextToBeStudiedTiles);
            nextToBeStudiedTiles.Clear();
        }
        
        return -1;
    }
}

public enum Axis { None, X, Y, Z }

public struct Face
{
    public Axis axis;
    public bool orientation;
}