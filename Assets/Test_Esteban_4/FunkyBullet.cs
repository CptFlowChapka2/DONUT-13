using UnityEngine;

public class FunkyBullet  : Bullet
{
    [Range(0,1)]public float strengh;
    protected override Vector3 BiasAddition(Vector3 force)
    {
        Vector3 perpendicular = Vector3.Cross(rb.linearVelocity, force);
        Quaternion rotation = Quaternion.FromToRotation(force, perpendicular);
        rotation = Quaternion.Slerp(Quaternion.LookRotation(force), rotation, strengh);
        return rotation*force;
    }
}
