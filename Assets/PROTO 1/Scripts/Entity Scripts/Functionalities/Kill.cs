using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Kill : Functionality
{
    public override void OnActivate()
    {
        return;
    }

    public override void OnDeActivate()
    {
        return;
    }

    public override void OnUpdate()
    {
        return;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (AppliableOnTheTriggeringEntity(this, other))
        {
            Entity e = other.gameObject.GetComponent<Entity>();
            Identity id = e.holderOfSingleTon.stateUpdater.functionalityAffectationDictionary.Keys.First(x => x.name == e.id);
            id.linkedEntities.Remove(e);
            thisEntity.holderOfSingleTon.entityDatabase.allEntities.Remove(e);
            Destroy(e.gameObject);
        }
    }
}
