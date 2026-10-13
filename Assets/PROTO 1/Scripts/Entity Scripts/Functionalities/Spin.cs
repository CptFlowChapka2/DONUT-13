using UnityEngine;

public class Spin : Functionality
{
    private float rotSpeed = 5.0f;
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
        transform.Rotate(transform.up, Time.deltaTime * rotSpeed);
    }
}
