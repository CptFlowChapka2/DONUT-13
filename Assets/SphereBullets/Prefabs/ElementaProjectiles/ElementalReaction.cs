using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class ElementalReaction : MonoBehaviour
{
    public int elementIndex;
    public ReactionManager reactmanager;
    public ProjectileManager spawner;

    private void Start()
    {
        reactmanager = FindAnyObjectByType<ReactionManager>();
        spawner = FindAnyObjectByType<ProjectileManager>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<ElementalReaction>())
        {
            int otherElementIndex = other.gameObject.GetComponent<ElementalReaction>().elementIndex;
            Vector2Int mixResult = new Vector2Int(elementIndex, otherElementIndex);
            
            if (!reactmanager.ballDico.ContainsKey(mixResult))
                return;
            
            // if (otherElementIndex == elementIndex)
            //     return;
            
            GameObject newBall = spawner.InstantiateAndInitialize(reactmanager.CallReaction(mixResult), transform.position, quaternion.identity);
            Destroy(gameObject);
        }
    }
}