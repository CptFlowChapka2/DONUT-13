using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class StateUpdater : MonoBehaviour
{
    [SerializeField] public Dictionary<Identity, List<SerializableType<Functionality>>> functionalityAffectationDictionary;
    public HolderOfSingleTon holderOfSingleTon;

    private void Awake()
    {
        holderOfSingleTon.stateUpdater = this;
    }

    public void Start()
    {
        foreach (var idFunctionalitiesPair in functionalityAffectationDictionary)
        {
            Identity identity = idFunctionalitiesPair.Key;
            foreach (Entity e in holderOfSingleTon.entityDatabase.allEntities)
            {
                if (e.id == identity.name) identity.linkedEntities.Add(e);
            }
        }
        
        UpdateAllEntitiesFunctionalities();
    }
    
    public void UpdateAllEntitiesFunctionalities()
    {
        foreach (var idFunctionalitiesPair in functionalityAffectationDictionary)
        {
            foreach (var entity in idFunctionalitiesPair.Key.linkedEntities)
            {
                foreach (Type type in entity.allFunctionalitiesRefs.Keys)
                {
                    entity.SetComponentEnable(type, idFunctionalitiesPair.Value.Contains(type));
                }
            } 
        }
    }
}

