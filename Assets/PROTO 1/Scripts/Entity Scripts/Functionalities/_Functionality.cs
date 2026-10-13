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

    public HashSet<IdentityName> GetIdsToActOn(Functionality functionality)
    {
        Identity id =
            thisEntity.holderOfSingleTon.stateUpdater.functionalityAffectationDictionary.Keys.First(x =>
                x.name == thisEntity.id);
        return id.actableIdentity[functionality.GetType()];
    }

    public Vector3 GetNearestEntityToActOnPosition(Functionality functionality)
    {
        HashSet<IdentityName> identities = GetIdsToActOn(functionality);
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
}