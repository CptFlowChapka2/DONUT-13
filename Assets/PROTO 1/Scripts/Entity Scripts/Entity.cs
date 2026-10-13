using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public  class Entity : MonoBehaviour
{
    public IdentityName id;
    public RendererAndPhysic rendererAndPhysic;
    public HolderOfSingleTon holderOfSingleTon;
    public Dictionary<Type, Functionality> allFunctionalitiesRefs = new Dictionary<Type, Functionality>();
   [NonSerialized]public UnityEvent<int,Collider> entityTriggerEvent = new UnityEvent<int,Collider>();
   public Rigidbody rb;
    
    public bool isStunned = false;
    private float stunTimerMax = 1f;
    private float stunTimer = 0f;

    public void ReceiveTriggerEvent(int i ,Collider c)
    {
        entityTriggerEvent.Invoke(i,c);
    }
    
    private void Awake()
    {
        holderOfSingleTon.entityDatabase.allEntities.Add(this);
    }

    private void Start()
    {
        Identity identity = holderOfSingleTon.stateUpdater.functionalityAffectationDictionary.Keys.First(x => x.name == id);
        foreach (Functionality component in gameObject.GetComponents<Functionality>())
        {
           allFunctionalitiesRefs.TryAdd(component.GetType(), component);
        }
        foreach (Type type in allFunctionalitiesRefs.Keys)
        {
            SetComponentEnable(type, holderOfSingleTon.stateUpdater.functionalityAffectationDictionary[identity].Contains(type));
        }
        rendererAndPhysic.ForcefullUpdateOfRenderParms();
    }

    public void UpdateAfterRuleChange()
    {
        rendererAndPhysic.ForcefullUpdateOfRenderParms();
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
        if (isStunned)
        {
            stunTimer += Time.deltaTime;
            if (stunTimer >= stunTimerMax)
            {
                stunTimer = 0f;
                stunTimerMax = 1f;
                isStunned = false;
            }
            return;
        }
        foreach (var functionality in allFunctionalitiesRefs.Values)
        {
            if (functionality.enabled) functionality.OnUpdate();
        }
    }
}