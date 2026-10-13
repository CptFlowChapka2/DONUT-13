using System.Collections.Generic;
using UnityEngine;

public class Stun : Functionality
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
    
    private void OnTriggerStay(Collider other)
    {
        if (AppliableOnTheTriggeringEntity(this, other))
        {
            other.gameObject.GetComponent<Entity>().isStunned = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (AppliableOnTheTriggeringEntity(this, other))
        {
            other.gameObject.GetComponent<Entity>().isStunned = false;
        }
    }
}
