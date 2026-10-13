using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Kill : Functionality
{
    public override void OnActivate()
    {
        thisEntity.entityTriggerEvent.AddListener(OnTriggerEvent);
    }

    public override void OnDeActivate()
    {
        thisEntity.entityTriggerEvent.RemoveListener(OnTriggerEvent);
    }

    public override void OnUpdate()
    {
        return;
    }
    
    

    public void OnTriggerEvent(int i, Collider c)
    {
        if (i == 0)
        {
            if (AppliableOnTheTriggeringEntity(this, c))
            {
               
                Entity e = c.gameObject.GetComponentInParent<Entity>();
                Identity id = e.holderOfSingleTon.stateUpdater.functionalityAffectationDictionary.Keys.First(x => x.name == e.id);
                id.linkedEntities.Remove(e);
                thisEntity.holderOfSingleTon.entityDatabase.allEntities.Remove(e);
                thisEntity.holderOfSingleTon.stateUpdater.playerScript.linkedEntities.Remove(e);
                Destroy(e.gameObject);
            }
        }
    }
}
