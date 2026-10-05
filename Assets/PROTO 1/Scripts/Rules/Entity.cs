using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public  class Entity : MonoBehaviour
{
    private Dictionary<Type, Fonctionality> allFonctionalitiesRefs;

    private void Start()
    {
        foreach (Fonctionality component in gameObject.GetComponents<Fonctionality>())
        {
            allFonctionalitiesRefs.TryAdd(component.GetType(),component);
        }
    }

    public void SetComponentEnable(Type t, bool b)
    {
        bool previouslyenabled = allFonctionalitiesRefs[t].enabled;
        if (!b && previouslyenabled) allFonctionalitiesRefs[t].OnDeActivate();
        allFonctionalitiesRefs[t].enabled = b;
        if (b && !previouslyenabled) allFonctionalitiesRefs[t].OnActivate();
    }

    private void Update()
    {
        foreach (var comp in allFonctionalitiesRefs.Values)
        {
            comp.OnUpdate();
        }
    }
}