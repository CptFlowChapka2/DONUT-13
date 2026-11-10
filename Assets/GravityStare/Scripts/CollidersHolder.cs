using System;
using System.Collections.Generic;
using UnityEngine;

public class CollidersHolder : MonoBehaviour
{
    #region Singleton

    public static CollidersHolder instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    #endregion
    
    public List<(ForceReceiver receiver, Collider collider)> colliders = new List<(ForceReceiver, Collider)>();

    public static ForceReceiver FindReceiverByCollider(Collider collider)
    {
        return instance.colliders.Find(x => x.collider == collider).receiver;
    }
}
