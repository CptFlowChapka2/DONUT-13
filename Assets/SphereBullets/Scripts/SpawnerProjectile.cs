using UnityEngine;

public class SpawnerProjectile : Projectile
{
    [Header("Spawner Properties")]
    [SerializeField] private GameObject projectilesToSpawn;
    [SerializeField] private float maxTimer;
    private float tSpawner;
    
    void Start()
    {
        base.Start();
    }
    
    void FixedUpdate()
    {
        base.FixedUpdate();
        SpawnProjectilesOnTwoSides(projectilesToSpawn);
    }

    private void SpawnProjectilesOnTwoSides(GameObject projectile)
    {
        tSpawner += Time.fixedDeltaTime;
        if (tSpawner >= maxTimer)
        {
            tSpawner = 0;
            Vector3 normal = transform.position.normalized;
            Vector3 rightDirection = Vector3.ProjectOnPlane(transform.right, normal).normalized;
            
            InstantiateAndInitialize(projectile, transform.position, Quaternion.LookRotation(rightDirection, normal));
            InstantiateAndInitialize(projectile, transform.position, Quaternion.LookRotation(-rightDirection, normal));
        }
    }

    private void InstantiateAndInitialize(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject go = Instantiate(prefab, position, rotation);
        go.GetComponent<Projectile>().distanceToSphere = Mathf.Abs(transform.position.magnitude);
    }
}
