using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Functionality : MonoBehaviour
{
    public Entity thisEntity;
    public abstract void OnActivate();
    public abstract void OnDeActivate();
    public abstract void OnUpdate();

    protected HashSet<IdentityName> GetIdsToActOn(Functionality functionality)
    {
        Identity id =
            thisEntity.holderOfSingleTon.stateUpdater.functionalityAffectationDictionary.Keys.First(x =>
                x.name == thisEntity.id);
        if (!id.actableIdentity.Keys.Contains(functionality.GetType()))
        {
            return null;
        }
        return id.actableIdentity[functionality.GetType()];
    }

    protected Vector3 GetPositionOfNearestEntityToActOn(Functionality functionality)
    {
        HashSet<IdentityName> identities = GetIdsToActOn(functionality);
        if(identities == null) return transform.position;
        if (identities.Count == 0) return transform.position;
        Vector3 target = new Vector3(Mathf.Infinity, 0, Mathf.Infinity);
        foreach (Entity e in thisEntity.holderOfSingleTon.entityDatabase.allEntities)
        {
            if (identities.Contains(e.id) && 
                (e.transform.position - this.transform.position).magnitude <
                (target - this.transform.position).magnitude)
            {
                target = e.transform.position;
            }
        }
        return target;
    }

    protected bool AppliableOnTheTriggeringEntity(Functionality functionality, Collider other)
    {
        HashSet<IdentityName> identities = GetIdsToActOn(functionality);
        if(identities == null) return false;
        return identities.Contains(other.gameObject.GetComponentInParent<Entity>().id);
    }
}