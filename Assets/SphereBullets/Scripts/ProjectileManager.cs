using System;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private Projectile[] allProjectiles = new Projectile[5];
    [SerializeField] private Transform spawnPoint;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            InstantiateAndInitialize(allProjectiles[0].gameObject, spawnPoint.position, transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            InstantiateAndInitialize(allProjectiles[1].gameObject, spawnPoint.position, transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 normal = transform.position.normalized;
            Vector3 spawnDirection = Vector3.ProjectOnPlane(transform.forward, normal).normalized;
            float deltaAngle = 360f / 8f;
            for (int i = 0; i < 8; i++)
            {
                InstantiateAndInitialize(allProjectiles[2].gameObject, spawnPoint.position, Quaternion.LookRotation(spawnDirection, normal));
                spawnDirection = Quaternion.AngleAxis(deltaAngle, normal) * spawnDirection;
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            InstantiateAndInitialize(allProjectiles[3].gameObject, spawnPoint.position, transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Vector3 normal = transform.position.normalized;
            Vector3 spawnDirection = Vector3.ProjectOnPlane(transform.forward, normal).normalized;
            float deltaAngle = 360f / 8f;
            for (int i = 0; i < 8; i++)
            {
                InstantiateAndInitialize(allProjectiles[4].gameObject, spawnPoint.position, Quaternion.LookRotation(spawnDirection, normal));
                spawnDirection = Quaternion.AngleAxis(deltaAngle, normal) * spawnDirection;
            }
        }
    }

    private void InstantiateAndInitialize(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject go = Instantiate(prefab, position, rotation);
        go.GetComponent<Projectile>().distanceToSphere = Mathf.Abs(spawnPoint.position.magnitude);
    }
}
