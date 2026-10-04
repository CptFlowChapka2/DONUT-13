using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityScript : MonoBehaviour
{
    private EntityPrefabHolder holder;
    public KeyWord currentKeyword;
    [SerializeField] private GameObject replacable;

    public Dictionary<string, object> allVerbParams = new Dictionary<string, object>();
    
    private void Start()
    {
        holder = FindAnyObjectByType<EntityPrefabHolder>();
        holder.allEntities.Add(this);
        replacable.GetComponents<Verb>().ToList().ForEach(x =>x.AddValues(ref allVerbParams));
    }

    private void OnDestroy()
    {
        holder.allEntities.Remove(this);
    }

    public bool AddVerb<T>() where T :Verb
    {
        if (TryGetComponent<T>(out var verb)) return false;
        T component=gameObject.AddComponent<T>();
        component.AddValues(ref allVerbParams);
        return true;
    }

    public void ReplaceIdentity(GameObject newIdentity)
    {
        var nI= Instantiate(newIdentity, this.transform);
        List<Verb> allComponentInNew=new List<Verb>();
        
        nI.GetComponents<Verb>(allComponentInNew);
        var defaultParamOverride = nI.gameObject.GetComponent<DefaultPreFabValues>();
        foreach (var pair in defaultParamOverride.defaultVerbsParamsOverride)
        {
            if (allVerbParams.ContainsKey(pair.Key))
            {
                allVerbParams[pair.Key] = pair.Value;
            }
            else
            {
                allVerbParams.TryAdd(pair.Key, pair.Value);
            }
        }

        currentKeyword = defaultParamOverride.actualKeyword;

        foreach (var verb in allComponentInNew)
        {
            verb.AddValues(ref allVerbParams);
        }
        
        
        Destroy(replacable);
        replacable = nI;
    }
    
    
}
