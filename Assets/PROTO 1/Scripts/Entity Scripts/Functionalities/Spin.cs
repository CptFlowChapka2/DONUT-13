using UnityEngine;

public class Spin : Functionality
{
    private float rotSpeed = 300.0f;
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
       
        thisEntity.rb.MoveRotation(thisEntity.rb.rotation * Quaternion.AngleAxis(Time.deltaTime * rotSpeed,transform.up));
    }
}
