using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class SpawnerDataAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    public Vector3 offset;
    public float spawnCD;

}

public struct SpawnerData : IComponentData
{
    public Entity Prefab;
    public float3 offset;
    public float3 origine;
    public float spawnCD;
}

class SpawnerBaker : Baker<SpawnerDataAuthoring>
{
    public override void Bake(SpawnerDataAuthoring authoring)
    {
        // This line converts the Spawner GameObject into an Entity.
        // TransformUsageFlags is None because the Spawner entity is not
        // rendered and does not need a LocalTransform component.
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity, new SpawnerData()
        {
            // This GetEntity call converts a GameObject prefab into an entity
            // prefab. The prefab is rendered, so it requires the standard Transform
            // components, that's why TransformUsageFlags is set to Dynamic.
            Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
            offset = authoring.offset,
            spawnCD = authoring.spawnCD,
            origine = authoring.transform.position
        });
    }
}
