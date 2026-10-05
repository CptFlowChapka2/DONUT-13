using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Identity
{
    public IdentityName name = IdentityName.None;
    
    [Header("Initial Parameters")]
    public bool blue = false;
    public bool red = false;
    
    [NonSerialized] public List<Entity> linkedEntities = new List<Entity>();
}

public enum IdentityName{None, Hero, Stone}