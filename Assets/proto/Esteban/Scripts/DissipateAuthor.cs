using Unity.Entities;
using UnityEngine;

public class DissipateAuthor : MonoBehaviour
{
    public float maxTime;
}

public struct Dissipate : IComponentData
{
    public float maxTime;
    public float currentTime;
}

public class DissipateBaker : Baker<DissipateAuthor>
{
    public override void Bake(DissipateAuthor authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity, new Dissipate()
        {
            maxTime = authoring.maxTime,
            currentTime = 0
        });
    }
}
