using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawn : Functionality
{
    //Il vaut mieux faire en sorte que "spawn" est limité à un seul paramètre de spawn par identité, sinon ça va être le bazar
    public GameObject prefabToSpawn;
    private float cd = 2.0f;
    private float distToSpawn = 2.0f;
    private float t = 0;
    
    public override void OnActivate()
    {
        return;
    }

    public override void OnDeActivate()
    {
        t = 0;
    }

    public override void OnUpdate()
    {
        t += Time.deltaTime;
        if (t > cd)
        {
            t = 0;
            //C'est si moche
            HashSet<IdentityName> identities = GetIdsToActOn(this);
            if(identities == null) return;
            GameObject prefab = Instantiate(prefabToSpawn, transform.position + (transform.forward * distToSpawn), transform.rotation);
            Entity e = prefab.GetComponent<Entity>();
            e.id = identities.First();
            Identity id = e.holderOfSingleTon.stateUpdater.functionalityAffectationDictionary.Keys.First(x => x.name == e.id);
            id.linkedEntities.Add(e);
            PlayerScript ps = e.holderOfSingleTon.stateUpdater.playerScript; 
            if (ps.linkedIdentity.Contains(e.id)) ps.linkedEntities.Add(e);
        }
    }
}
