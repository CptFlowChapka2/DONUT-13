using System;
using UnityEngine;

public class Points : MonoBehaviour
{
    public LinePooledScript mommy;
    public float timer = 1;
    private float internalTimer = 1;
    
    
    private void Update()
    {
        internalTimer -= Time.deltaTime;
        if(internalTimer<=0&& gameObject.activeSelf)
        {
            internalTimer = timer;
            mommy.points.Release(this);
        }
    }
}
