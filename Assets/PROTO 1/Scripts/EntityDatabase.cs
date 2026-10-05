using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityDatabase : MonoBehaviour
{
    public static EntityDatabase Instance;
    public List<Entity> allEntities = new List<Entity>();

    private void Awake()
    {
        Instance = this;
    }
}
