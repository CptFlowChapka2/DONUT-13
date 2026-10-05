using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Identity
{
    public string name = "Placeholder";
    [NonSerialized] public List<Entity> linkedEntities = new List<Entity>();
    //todo = + liste de param par default
}