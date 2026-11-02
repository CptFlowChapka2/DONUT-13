using System;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private SliderController sliderController;
    [SerializeField] private Projectile[] allProjectiles = new Projectile[6];
    [SerializeField] private Transform spawnPoint;

    private GameObject currentCam;

    private void Start()
    {
        currentCam = mainCam.gameObject;
    }

    private void Update()
    {
        int nbInstances = 1;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) nbInstances = 8;

        GameObject[] allSpawns = new GameObject[nbInstances];

        if (Input.GetKeyDown(KeyCode.Q))
            allSpawns = MultiInstantiateAndInitialize(allProjectiles[0].gameObject, nbInstances);

        if (Input.GetKeyDown(KeyCode.W))
            allSpawns = MultiInstantiateAndInitialize(allProjectiles[1].gameObject, nbInstances);

        if (Input.GetKeyDown(KeyCode.E))
        {
            allSpawns = MultiInstantiateAndInitialize(allProjectiles[2].gameObject, nbInstances);
            // if (Input.GetKey(KeyCode.Space))
            // {
            //     SoundManager.instance.deltaTest.Stop();
            //     SoundManager.instance.deltaTest.Play();
            // }
        }
        
        if (Input.GetKeyDown(KeyCode.R))
            allSpawns = MultiInstantiateAndInitialize(allProjectiles[3].gameObject, nbInstances);

        if (Input.GetKeyDown(KeyCode.T))
            allSpawns = MultiInstantiateAndInitialize(allProjectiles[4].gameObject, nbInstances);

        if (Input.GetKeyDown(KeyCode.Y))
            allSpawns = MultiInstantiateAndInitialize(allProjectiles[5].gameObject, nbInstances);

        if (Input.GetKey(KeyCode.LeftControl) && allSpawns[0])
        {
            Projectile projectile = allSpawns[0].GetComponent<Projectile>();
            TakeControl(projectile.personalCamera);
            sliderController.gameObject.SetActive(true);
            sliderController.toControl = projectile;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            sliderController.gameObject.SetActive(false);
            TakeControl(mainCam);
        }
    }

    public void TakeControl(Camera toFollow)
    {
        currentCam.SetActive(false);
        currentCam = toFollow.gameObject;
        currentCam.SetActive(true);
    }
    
    private GameObject[] MultiInstantiateAndInitialize(GameObject prefab, int nbOfInstances)
    {
        GameObject[] allSpawns = new GameObject[nbOfInstances];
        Vector3 normal = transform.position.normalized;
        Vector3 spawnDirection = Vector3.ProjectOnPlane(transform.forward, normal).normalized;
        float deltaAngle = 360f / (float)nbOfInstances;
        for (int i = 0; i < nbOfInstances; i++)
        {
            GameObject go = InstantiateAndInitialize(prefab, spawnPoint.position, Quaternion.LookRotation(spawnDirection, normal));
            allSpawns[i] = go;
            spawnDirection = Quaternion.AngleAxis(deltaAngle, normal) * spawnDirection;
        }
        return allSpawns;
    }

    private GameObject InstantiateAndInitialize(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject go = Instantiate(prefab, position, rotation);
        go.GetComponent<Projectile>().distanceToSphere = Mathf.Abs(spawnPoint.position.magnitude);
        return go;
    }
}
