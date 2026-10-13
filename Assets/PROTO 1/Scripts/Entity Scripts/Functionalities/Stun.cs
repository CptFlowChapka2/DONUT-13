using System.Collections.Generic;
using UnityEngine;

public class Stun : Functionality
{
    private float securityDist = 0.07f;
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
            var e = c.gameObject.GetComponentInParent<Entity>();
            if(!e)return;
           
            e.isStunned = true;
            Vector3 distBeetween = c.transform.position - transform.position;
            e.rb.MovePosition(c.attachedRigidbody.position+ distBeetween.normalized * securityDist);
            
        }
        else if (i == 2)
        {
            var e = c.gameObject.GetComponentInParent<Entity>();
            if(!e)return;
                
                e.isStunned = false;
            
        }
    }
}
