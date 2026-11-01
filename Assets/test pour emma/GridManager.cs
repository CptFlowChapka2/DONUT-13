using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{
    public CaseScript[,] grid;
    public Vector2Int gridSize;
    public float wallPropabilityOto100;
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
                var r = Random.value;
                grid[i,j]=Instantiate(prefabCase, new Vector3(i * prefabsize, 0,j * prefabsize), Quaternion.identity).GetComponent<CaseScript>();
                if (r > wallPropabilityOto100*0.01) grid[i, j].Mur = true;

            }
        }

        player.transform.position = new Vector3(playerPos.x * prefabsize, 1, playerPos.y * prefabsize);
    }


    public bool VerifyNextPost(Vector2Int input)
    {
        var test = playerPos + input;
        test = new Vector2Int(Mathf.Clamp(test.x, 0, gridSize.x), Mathf.Clamp(test.y, 0, gridSize.y));
        Debug.Log(test+" "+grid[test.x, test.y].Mur);
        return grid[test.x, test.y].Mur;
    }
    public void MovePlayer(Vector2Int input)
    {
        
        playerPos += input;
        playerPos = new Vector2Int(Mathf.Clamp(playerPos.x, 0, gridSize.x), Mathf.Clamp(playerPos.y, 0, gridSize.y));
        player.transform.position = new Vector3(playerPos.x * prefabsize , 1, playerPos.y * prefabsize );
    }
}
