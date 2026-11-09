using System;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class LinePooledScript : MonoBehaviour
{
    [SerializeField] private float timerToNextPoint = 0.1f;
    private float internalTimerToNextPoint = 0.1f;
    public GameObject prefab;
    public Camera cam;
    public CustomObjectPool<Points> points1;
    public CustomObjectPool<Points> points2;
    private Vector3 lastPos1;
    private Vector3 lastPos2;
    public int maxCapacity = 15;
    public float forwardDistanceBetweenPoints = 1;
    public float cameraDistance = 7.5f;
    public float cameraHeight = 1.2f;
    public float cameraXRotation = 0f;

    public GameObject playerObject;
    private LineRenderer lineRenderer;

    public CameraParametersScriptableObject cameraParameters;

    private PlayerInput playerInput;
    private InputAction leftStick;
    private InputAction rightStick;
    private InputAction lineSwitch;

    public LineRenderer lineRenderer1;
    public LineRenderer lineRenderer2;

    public GameObject playerPrefab;

    private bool lineTwoBool;

    #region Pooling

    private void Awake()
    {
        points1 = new CustomObjectPool<Points>(
            CreateItem, maxCapacity
        );
        points2 = new CustomObjectPool<Points>(
            CreateItem, maxCapacity
        );
        lastPos1 = transform.position + Vector3.left;
        lastPos2 = transform.position + Vector3.right;
        internalTimerToNextPoint = timerToNextPoint;

        lineRenderer1.positionCount = maxCapacity;
        lineRenderer2.positionCount = maxCapacity;

        playerInput = GetComponent<PlayerInput>();
        leftStick = playerInput.actions["LeftStick"];
        rightStick = playerInput.actions["RightStick"];
        lineSwitch = playerInput.actions["LineSwitch"];

        for (int i = 0; i < maxCapacity; i++)
        {
            points1.Get();
            points2.Get();
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
            cam.transform.position = UpdatePooledObject(points1, leftStick, ref lastPos1);
            UpdatePooledObject(points2, rightStick, ref lastPos2);
            UpdateLineRenderer(lineRenderer1, points1);
            UpdateLineRenderer(lineRenderer2, points2);
        }

        if (!lineTwoBool)
        {
            playerPrefab.transform.position = points1.unavailableObjects[maxCapacity - 46].transform.position;
        }
        else
        {
            playerPrefab.transform.position = points2.unavailableObjects[maxCapacity - 46].transform.position;
        }

        if (lineSwitch.WasPerformedThisFrame())
        {
            lineTwoBool = !lineTwoBool;
        }
    }

    private Vector3 UpdatePooledObject(CustomObjectPool<Points> pool, InputAction inputAction, ref Vector3 lastPos)
    {
        Points nextPoint = pool.Get();

        Vector3 inputPos = ((Vector3.forward * forwardDistanceBetweenPoints) / 5) +
                           new Vector3(inputAction.ReadValue<Vector2>().x / 5, inputAction.ReadValue<Vector2>().y / 5,
                               0);

        nextPoint.transform.position = lastPos + inputPos;
        lastPos = nextPoint.transform.position;
        Vector3 newCamPos = new Vector3(0, cameraHeight, nextPoint.transform.position.z - cameraDistance);

        return newCamPos;
    }

    private void UpdateLineRenderer(LineRenderer lr, CustomObjectPool<Points> pool)
    {
        for (int i = pool.CountInactive; i > 0; i--)
        {
            if (pool.CountAll < maxCapacity)
                return;

            lr.SetPosition(maxCapacity - i, pool.unavailableObjects[i - 1].transform.position);
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