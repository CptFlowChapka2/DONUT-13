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
        transform.Translate(transform.forward * (Time.deltaTime * speed));
    }
}
