using UnityEngine;

public class Follow : Functionality
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
        Vector3 target = GetPositionOfNearestEntityToActOn(this);
        if(target==Vector3.zero)return;
        thisEntity.rb.MovePosition(thisEntity.rb.position+ (target - transform.position).normalized * (Time.deltaTime * speed));
    }
}
