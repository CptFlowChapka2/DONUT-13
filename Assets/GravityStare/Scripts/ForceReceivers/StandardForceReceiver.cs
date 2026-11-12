using System.Collections.Generic;
using UnityEngine;

public class StandardForceReceiver : ForceReceiver
{
    protected override Vector3 BiasAddition(KeyValuePair<ForceApplier, Vector3> force)
    {
        return force.Value;
    }
}
