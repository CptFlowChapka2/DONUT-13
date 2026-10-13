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
        transform.LookAt(GetNearestEntityToActOnPosition(this));
    }
}
