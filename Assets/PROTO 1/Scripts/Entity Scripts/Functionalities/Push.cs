using System;
using System.Collections.Generic;
using UnityEngine;

public class Push : Functionality
{
    private float securityDist = 5.0f;
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
            Vector3 distBeetween = other.transform.position - transform.position;
            //if il n'y a pas le bord de la map juste après l'objet:
            other.gameObject.GetComponentInParent<Transform>().position = transform.position + distBeetween.normalized * securityDist;
        }
    }
}
