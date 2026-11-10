using UnityEngine;

public class StandardForceReceiver : ForceReceiver
{
    protected override Vector3 BiasAddition(Vector3 force)
    {
        return force;
    }
}
