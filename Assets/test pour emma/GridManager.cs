using System;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public CaseScript[,] grid;
    public Vector2Int gridSize;
    public GameObject prefabCase;
    public GameObject player;
    public Vector2Int playerPos;
    public float prefabsize;
    public Camera camera;


    private void Start()
    {
        grid = new CaseScript[gridSize.x, gridSize.y];
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                grid[i,j]=Instantiate(prefabCase, new Vector3(i * prefabsize, 0,j * prefabsize), Quaternion.identity).GetComponent<CaseScript>();
                
            }
        }

        player.transform.position = new Vector3(playerPos.x * prefabsize, 1, playerPos.y * prefabsize);

        //camera.transform.position = new Vector3(gridSize.x * prefabsize, 15, gridSize.y * prefabsize);
    }

    public void MovePlayer(Vector2Int input)
    {
        playerPos += input;
        playerPos = new Vector2Int(Mathf.Clamp(playerPos.x, 0, gridSize.x), Mathf.Clamp(playerPos.y, 0, gridSize.y));
        player.transform.position = new Vector3(playerPos.x * prefabsize , 1, playerPos.y * prefabsize );
    }
}
