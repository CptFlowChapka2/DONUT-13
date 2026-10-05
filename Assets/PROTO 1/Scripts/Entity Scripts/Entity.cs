using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public  class Entity : MonoBehaviour
{
    public IdentityName id;
    public Dictionary<Type, Functionality> allFunctionalitiesRefs = new Dictionary<Type, Functionality>();

    private void Start()
    {
        foreach (Functionality component in gameObject.GetComponents<Functionality>())
        {
            allFunctionalitiesRefs.TryAdd(component.GetType(),component);
        }
    }

    public void SetComponentEnable(Type t, bool b)
    {
        bool previouslyEnabled = allFunctionalitiesRefs[t].enabled;
        if (!b && previouslyEnabled) allFunctionalitiesRefs[t].OnDeActivate();
        allFunctionalitiesRefs[t].enabled = b;
        if (b && !previouslyEnabled) allFunctionalitiesRefs[t].OnActivate();
    }

    private void Update()
    {
        foreach (var functionality in allFunctionalitiesRefs.Values)
        {
            if (functionality.enabled) functionality.OnUpdate();
        }
    }
}