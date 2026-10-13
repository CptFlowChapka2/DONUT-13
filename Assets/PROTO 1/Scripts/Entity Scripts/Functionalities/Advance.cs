using UnityEngine;

public class Advance : Functionality
{
    private float speed = 5.0f;
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
        thisEntity.rb.MovePosition(thisEntity.rb.position+ transform.forward * (Time.fixedDeltaTime * speed));
        //Check out of bounds
    }
}
