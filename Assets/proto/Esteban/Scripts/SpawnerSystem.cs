using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public partial struct SpawnerSystem : ISystem
{
    private float nextSpawn;
    private Random random;
    
    public void OnCreate(ref SystemState state)
    {
        // This call prevents the system from updating unless at least one entity with
        // the Spawner component exists in the ECS world.
        // This also prevents GetSingleton from throwing an exception if it doesn't find
        // an object of type Spawner.
        state.RequireForUpdate<SpawnerData>();
        random = new Random((uint)System.DateTime.Now.Ticks);
    }
    
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        
        SpawnerData spawnerData = SystemAPI.GetSingleton<SpawnerData>();
        
        if (!(nextSpawn < SystemAPI.Time.ElapsedTime)) return;
        
        Entity newEntity = state.EntityManager.Instantiate(spawnerData.Prefab);
        
        float3 randomOffset = (random.NextFloat3() - 0.5f) * 5f;
        randomOffset.y = 0;
        
        float3 newPosition = spawnerData.origine+spawnerData.offset + randomOffset;
        
        state.EntityManager.SetComponentData(newEntity,
            LocalTransform.FromPosition(newPosition));

        nextSpawn = (float)SystemAPI.Time.ElapsedTime + spawnerData.spawnCD;
        

    }

}
