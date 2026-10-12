using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerScript
{
    public List<IdentityName> linkedIdentity = new List<IdentityName>();
    [NonSerialized] public Dictionary<SerializableType<Functionality>, HashSet<IdentityName>> actableIdentity = 
        new Dictionary<SerializableType<Functionality>, HashSet<IdentityName>>();

    public HashSet<SerializableType<Functionality>> associatedFunc = new HashSet<SerializableType<Functionality>>();
    [NonSerialized] public HashSet<Entity> linkedEntities = new HashSet<Entity>();


    public void ClearTruth()
    {
        linkedIdentity = new List<IdentityName>();
        actableIdentity = new Dictionary<SerializableType<Functionality>, HashSet<IdentityName>>();
        associatedFunc = new HashSet<SerializableType<Functionality>>();
    }
}
