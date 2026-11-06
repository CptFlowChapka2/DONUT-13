using System;
using UnityEngine;
using UnityEngine.Pool;

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

    public GameObject playerObject;
    private LineRenderer lineRenderer;

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
            cam.transform.position = new Vector3(0, 1.2f, nextPoint.transform.position.z - 4.5f);
        }

        for (int i = points.CountInactive; i > 0; i--)
        {
            
            if (points.CountAll < maxCapacity)
                return;
            
            lineRenderer.SetPosition(maxCapacity - i, points.unavailableObjects[i - 1].transform.position);
        }
    }
}