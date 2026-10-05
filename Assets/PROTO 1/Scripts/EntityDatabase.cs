using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityDatabase : MonoBehaviour
{
    public HolderOfSingleTon holderOfSingleTon;
    public List<Entity> allEntities = new List<Entity>();

    private void Awake()
    {
        holderOfSingleTon.entityDatabase = this;
    }
}
