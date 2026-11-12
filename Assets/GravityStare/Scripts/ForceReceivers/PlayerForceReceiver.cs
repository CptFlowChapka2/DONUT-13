using System.Collections.Generic;
using UnityEngine;

public class PlayerForceReceiver : ForceReceiver
{
    protected override Vector3 BiasAddition(KeyValuePair<ForceApplier, Vector3> force)
    {
        if (force.Key is PlayerMovement)
        {
            return Vector3.zero;
        }
        else
        {
            return force.Value;
        }
    }
}
