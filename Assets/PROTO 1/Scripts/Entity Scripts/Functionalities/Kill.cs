using System.Collections.Generic;
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
        HashSet<IdentityName> identities = GetIdsToActOn(this);
        if (identities.Contains(other.gameObject.GetComponentInParent<Entity>().id))
        {
            //kill
        }
    }
}
