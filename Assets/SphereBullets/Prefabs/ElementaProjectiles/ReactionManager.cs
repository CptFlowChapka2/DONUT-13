using System.Collections.Generic;
using UnityEngine;

public class ReactionManager : MonoBehaviour
{
    public Dictionary<Vector2Int, GameObject> ballDico;
    public List<Vector2Int> reactionList;
    public List<GameObject> reactionprefabs;
    void Start()
    {
        ballDico = new Dictionary<Vector2Int, GameObject>();
        
        for (int i = 0; i < reactionList.Count; i ++)
        { 
            ballDico.Add(reactionList[i], reactionprefabs[i]);
        }
    }
    
    public GameObject CallReaction(Vector2Int reactionIndex)
    {
        return ballDico[reactionIndex];
    }
}
