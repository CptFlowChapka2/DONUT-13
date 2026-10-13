using System.Collections.Generic;
using UnityEngine;

public class LookAt : Functionality
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

        Vector3 v = GetPositionOfNearestEntityToActOn(this);
        if(v == transform.position)return;
        transform.rotation= Quaternion.LookRotation((v - transform.position).normalized);
    }
}
