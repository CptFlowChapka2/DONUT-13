using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] public Dictionary<Vector2, Tile> tiles = new Dictionary<Vector2, Tile>();
}
