using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;



public struct Slot : IComponentData
{
    public Entity value;
}

public class SlotAuthor : MonoBehaviour
{
    public GameObject Prefab;
}


class SlotBaker : Baker<SlotAuthor>
{
    public override void Bake(SlotAuthor authoring)
    {
        
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity, new Slot()
        {
            value = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
        });
    }
}