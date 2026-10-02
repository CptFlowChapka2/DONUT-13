using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;


public partial struct DissipateSystem : ISystem
{
    private EntityManager entityManager;
    
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Dissipate>();

        entityManager = state.EntityManager;

    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer dog =new EntityCommandBuffer(Allocator.TempJob);
        foreach (var disRef in SystemAPI.Query<RefRW<Dissipate>>().WithEntityAccess())
        {
            disRef.Item1.ValueRW.currentTime += Time.deltaTime;
            if (disRef.Item1.ValueRW.currentTime >= disRef.Item1.ValueRW.maxTime)
            {
                dog.DestroyEntity(disRef.Item2);
            }
            
        }   
        dog.Playback(entityManager);
        dog.Dispose();
    }
}
