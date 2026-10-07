using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

    public void TransformEntitiesToNewIdentity(IdentityName from,IdentityName to)
    {
        Identity fromKey =  functionalityAffectationDictionary.Keys.First(x=>x.name==from);
        Identity toKey =  functionalityAffectationDictionary.Keys.First(x=>x.name==to);
        
        toKey.linkedEntities.AddRange(fromKey.linkedEntities);
        fromKey.linkedEntities.Clear();
        toKey.linkedEntities.ForEach(x=>x.id=toKey.name);
        
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

    public void ClearTruth()
    {
        foreach (var idFunctionalitiesPair in functionalityAffectationDictionary)
        {
            functionalityAffectationDictionary[idFunctionalitiesPair.Key].Clear();
            foreach (var fieldInfo in idFunctionalitiesPair.Key.GetType().GetFields())
            {
                if (fieldInfo.FieldType == typeof(bool))
                {
                    fieldInfo.SetValue(idFunctionalitiesPair.Key,false);
                }

                if (fieldInfo.FieldType == typeof(Dictionary<SerializableType<Functionality>, List<IdentityName>>))
                {
                    fieldInfo.SetValue(idFunctionalitiesPair.Key,new Dictionary<SerializableType<Functionality>, List<IdentityName>>());
                }
                
            }
        } 
    }

    public void UpdateParamField(IdentityName iname,string name, object value)
    {
        Identity key =  functionalityAffectationDictionary.Keys.First(x=>x.name==iname);
        key.GetType().GetField(name).SetValue(key, value);
    }

    public void UpdateActableIdentity(IdentityName iname,SerializableType<Functionality> func,IdentityName toAdd)
    {
        Identity key =  functionalityAffectationDictionary.Keys.First(x=>x.name==iname);
        if (key.actableIdentity.TryAdd(func, new List<IdentityName>() ))
        {
            key.actableIdentity[func].Add(toAdd);
            return;
        }
        key.actableIdentity[func].Add(toAdd);
    }

    public void UpdateIdentityFonctionality(IdentityName identityName,Type t)
    {
        
          Identity key =  functionalityAffectationDictionary.Keys.First(x=>x.name==identityName);
          functionalityAffectationDictionary[key].Add(t);
          
         
         
    }
}

