using System;
using System.Collections.Generic;
using UnityEngine;

public class Push : Functionality
{
    private float securityDist = 0.1f;
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
        if (i == 1)
        {
            if (AppliableOnTheTriggeringEntity(this, c))
            {
                Vector3 distBeetween = c.transform.position - transform.position;
                //if il n'y a pas le bord de la map juste après l'objet:
                c.attachedRigidbody.MovePosition(c.attachedRigidbody.position+ distBeetween.normalized * securityDist);
            }
        }
    }
}
