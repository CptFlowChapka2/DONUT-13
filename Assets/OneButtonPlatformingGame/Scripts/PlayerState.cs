using System;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    public float stateTimeFrame = 1f;
    
    public virtual void OnEnter() { }
    public virtual void OnUpdate() { }
    public virtual void OnExit() { }
    
    
}
