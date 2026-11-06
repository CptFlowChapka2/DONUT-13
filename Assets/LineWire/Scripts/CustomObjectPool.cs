using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class CustomObjectPool<T> : IObjectPool<T> where T : class
{
    public List<T> unavailableObjects = new List<T>();
    public List<T> availableObjects = new List<T>();
    private int maxCapacity = 15;
    private Func<T> createFunction;

    public CustomObjectPool(Func<T> cf, int mc)
    {
        createFunction = cf;
        maxCapacity = mc;
    }

    public T Get()
    {
        if (unavailableObjects.Count >= maxCapacity)
        {
            var oldestItem = unavailableObjects.First();
            unavailableObjects.Remove(oldestItem);
            unavailableObjects.Add(oldestItem);
            return oldestItem;
        }
        else if (availableObjects.Count > 0)
        {
            var item = availableObjects.First();
            availableObjects.Remove(item);
            unavailableObjects.Add(item);
            return item;
        }

        var ret = createFunction();
        unavailableObjects.Add(ret);
        return ret;
    }

    public PooledObject<T> Get(out T v)
    {
        v = Get();
        return new PooledObject<T>(v, this);
    }

    public void Release(T element)
    {
        availableObjects.Add(element);
        unavailableObjects.Remove(element);
    }

    public void Clear()
    {
        availableObjects.Clear();
        unavailableObjects.Clear();
    }

    public int MaxCapacity
    {
        get => maxCapacity;
    }

    public int CountInactive
    {
        get => unavailableObjects.Count;
    }

    public int CountAvailable
    {
        get => availableObjects.Count;
    }

    public int CountAll
    {
        get => CountAvailable + CountInactive;
    }
}