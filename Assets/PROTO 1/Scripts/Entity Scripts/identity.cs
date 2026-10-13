using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Identity
{
    public IdentityName name = IdentityName.None;
    [SerializeField] public GameObject appearence;
    
    [NonSerialized] public Dictionary<SerializableType<Functionality>, HashSet<IdentityName>> actableIdentity = 
        new Dictionary<SerializableType<Functionality>, HashSet<IdentityName>>();
    
    [NonSerialized] public HashSet<Entity> linkedEntities = new HashSet<Entity>();
}

public enum IdentityName
{None, Hero, Stone,Arrow,Wall,Fire,Monster,Tree}