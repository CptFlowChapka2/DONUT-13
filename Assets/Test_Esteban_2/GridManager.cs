using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{
    public CaseScript[,] grid;
    public Vector2Int gridSize;

    public GameObject prefabCase;
    public float prefabsize;
    public float delay=5;
    public float reDelay=5;

    private void Start()
    {
        grid = new CaseScript[gridSize.x, gridSize.y];
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                var r = Random.value;
                grid[i, j] =
                    Instantiate(prefabCase, new Vector3(i * prefabsize, 0, j * prefabsize), Quaternion.identity)
                        .GetComponent<CaseScript>();
                grid[i, j].value = Random.value;
            }
        }
    }

    private void Update()
    {
        if (delay > 0)
        {
            delay += -Time.deltaTime;
            return;
        }
        UpdateWholeArray();
        delay = reDelay;
    }

    float totalDelta = 0;
    private void UpdateWholeArray()
    {
        totalDelta = 0;
        (CaseScript cS ,float delta)[] neighoor = new (CaseScript cS ,float delta)[8];
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                neighoor[0] = CalculateDeltaToNeighboor(new Vector2Int(i, j), new Vector2Int(i, j) + Vector2Int.up);
                neighoor[1] = CalculateDeltaToNeighboor(new Vector2Int(i, j), new Vector2Int(i, j) + Vector2Int.up+Vector2Int.right);
                neighoor[2] = CalculateDeltaToNeighboor(new Vector2Int(i, j), new Vector2Int(i, j) + Vector2Int.up+Vector2Int.left);
                neighoor[3] = CalculateDeltaToNeighboor(new Vector2Int(i, j), new Vector2Int(i, j) + Vector2Int.down);
                neighoor[4] = CalculateDeltaToNeighboor(new Vector2Int(i, j), new Vector2Int(i, j) + Vector2Int.down+Vector2Int.right);
                neighoor[5] = CalculateDeltaToNeighboor(new Vector2Int(i, j), new Vector2Int(i, j) + Vector2Int.down+Vector2Int.left);
                neighoor[6] = CalculateDeltaToNeighboor(new Vector2Int(i, j), new Vector2Int(i, j) + Vector2Int.left);
                neighoor[7] = CalculateDeltaToNeighboor(new Vector2Int(i, j), new Vector2Int(i, j) + Vector2Int.right);

                
                for (int k = 0; k < neighoor.Length; k++)
                {
                    if(neighoor[k].delta==0|| grid[i,j].value==0)continue;

                    var change =neighoor[k].cS.maxPerdition * (neighoor[k].delta / totalDelta);
                    grid[i,j].SetValue(Mathf.Clamp(grid[i,j].value-change,0,1));
                    neighoor[k].cS.SetValue( Mathf.Clamp(neighoor[k].cS.value +change,0,1));
                }
                
            }
        }
        
    }

    public (CaseScript,float) CalculateDeltaToNeighboor(Vector2Int coords,Vector2Int to)
    {
        if (to.x >= gridSize.x || to.x < 0 || to.y >= gridSize.y||to.y<0) return (null,0);
        var delta = grid[to.x, to.y].value - grid[coords.x, coords.y].value;
        if (Mathf.Sign(delta) == 1)return (grid[to.x, to.y],delta);
        totalDelta += delta;
        return (grid[to.x, to.y],delta);
    }

    
}