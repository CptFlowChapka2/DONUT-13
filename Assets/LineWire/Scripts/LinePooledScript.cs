using System;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class LinePooledScript : MonoBehaviour
{
    [SerializeField] private float timerToNextPoint = 0.1f;
    private float internalTimerToNextPoint = 0.1f;
    public GameObject prefab;
    public Camera cam;
    public CustomObjectPool<Points> points;
    private Vector3 lastPos;
    public int maxCapacity = 15;
    public float forwardDistanceBetweenPoints = 1;
    public float cameraDistance = 7.5f;
    public float cameraHeight = 1.2f;
    public float cameraXRotation = 0f;

    public GameObject playerObject;
    private LineRenderer lineRenderer;
    
    public CameraParametersScriptableObject cameraParameters;

    #region Pooling

    private void Awake()
    {
        points = new CustomObjectPool<Points>(
            CreateItem, maxCapacity
        );
        lastPos = transform.position;
        internalTimerToNextPoint = timerToNextPoint;
        
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = maxCapacity;

        for (int i = 0; i < maxCapacity; i++)
        {
            points.Get();
        }

        if (cameraParameters != null)
        {
            cam.fieldOfView = cameraParameters.fov;
            cameraDistance = cameraParameters.cameraDistance;
            cameraHeight = cameraParameters.cameraHeight;
            cameraXRotation = cameraParameters.cameraXRotation;
            cam.transform.rotation = Quaternion.Euler(cameraXRotation, 0, 0);
        }
    }

    private Points CreateItem()
    {
        Points pooledObject = GameObject.Instantiate(prefab).GetComponent<Points>();
        pooledObject.name = "PooledPoint";
        pooledObject.mommy = this;
        return pooledObject;
    }

    #endregion


    void Update()
    {
        RestartScene();
        
        if (internalTimerToNextPoint > 0)
        {
            internalTimerToNextPoint -= Time.deltaTime;
        }

        if (internalTimerToNextPoint <= 0)
        {
            internalTimerToNextPoint = timerToNextPoint;
            Points nextPoint = points.Get();
            Vector3 inputPos = ((Vector3.forward * forwardDistanceBetweenPoints) / 5) +
                               new Vector3(Input.GetAxis("Horizontal") / 5, Input.GetAxis("Vertical") / 5,
                                   0);
            nextPoint.transform.position = lastPos + inputPos;
            lastPos = nextPoint.transform.position;
            cam.transform.position = new Vector3(0, cameraHeight, nextPoint.transform.position.z - cameraDistance);
        }

        for (int i = points.CountInactive; i > 0; i--)
        {
            
            if (points.CountAll < maxCapacity)
                return;
            
            lineRenderer.SetPosition(maxCapacity - i, points.unavailableObjects[i - 1].transform.position);
        }
    }

    private void RestartScene()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}