using UnityEngine;

public class StandartBullet : Bullet
{
    

    protected override Vector3 BiasAddition(Vector3 force)
    {
        return force;
    }
    
}
