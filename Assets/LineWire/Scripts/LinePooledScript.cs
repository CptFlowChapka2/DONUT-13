
using System;
using UnityEngine;
using UnityEngine.Pool;

public class LinePooledScript : MonoBehaviour
{
    [SerializeField] private float timerToNextPoint = 0.1f;
     private float internalTimerToNextPoint = 0.1f;
    public GameObject prefab;
    public Camera cam;
    public ObjectPool<Points> points;
    private Vector3 lastPos;

    #region Pooling

    private void Awake()
    {
        points= new ObjectPool<Points>(
            createFunc: CreateItem,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: OnDestroyItem,
            collectionCheck: true,   // helps catch double-release mistakes
            defaultCapacity: 15,
            maxSize: 15
        );
        lastPos = transform.position;
        internalTimerToNextPoint = timerToNextPoint;
    }
    
    private Points CreateItem()
    {
        Points pooledObject = GameObject.Instantiate(prefab).GetComponent<Points>();
        pooledObject.name = "PooledPoint";
        pooledObject.mommy = this;
        pooledObject.gameObject.SetActive(false);
        return pooledObject;
    }

    // Called when an item is taken from the pool.
    private void OnGet(Points pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    // Called when an item is returned to the pool.
    private void OnRelease(Points pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    // Called when the pool decides to destroy an item (e.g., above max size).
    private void OnDestroyItem(Points pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }

    #endregion


    void Update()
    {
        Debug.Log(points.CountAll);
        if (internalTimerToNextPoint > 0)
        {
            internalTimerToNextPoint -= Time.deltaTime;
        }

        if (internalTimerToNextPoint <= 0)
        {
            internalTimerToNextPoint = timerToNextPoint;
            Points nextPoint = points.Get();
            nextPoint.transform.position =  lastPos + (Vector3.forward / 5) + new Vector3(Input.GetAxis("Horizontal") / 5, Input.GetAxis("Vertical") / 5, 0);
            lastPos = nextPoint.transform.position;
            cam.transform.position = new Vector3(0, 1.2f, nextPoint.transform.position.z - 3.5f);
        }
        
    }
}