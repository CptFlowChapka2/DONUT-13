using UnityEngine;

public class TangentProjectile : Projectile
{
    [SerializeField] private GameObject projectilesToSpawn;
    [SerializeField] private float maxTimer;
    [SerializeField] private Transform[] spawnPoints;
    private float t;
    
    void Start()
    {
        base.Start();
    }
    
    void FixedUpdate()
    {
        base.FixedUpdate();
        rb.linearVelocity = transform.forward * speed;
        t += Time.fixedDeltaTime;
        if (t >= maxTimer)
        {
            t = 0;
            Vector3 normal = transform.position.normalized;
            Vector3 rightDirection = Vector3.ProjectOnPlane(transform.right, normal).normalized;
            
            InstantiateAndInitialize(projectilesToSpawn, transform.position, Quaternion.LookRotation(rightDirection, normal));
            InstantiateAndInitialize(projectilesToSpawn, transform.position, Quaternion.LookRotation(-rightDirection, normal));
        }
    }
    
    private void InstantiateAndInitialize(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject go = Instantiate(prefab, position, rotation);
        go.GetComponent<Projectile>().distanceToSphere = Mathf.Abs(transform.position.magnitude);
    }
}
