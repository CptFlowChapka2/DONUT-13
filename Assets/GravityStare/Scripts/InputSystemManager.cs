using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemManager : MonoBehaviour
{
    #region Singleton

    public static InputSystemManager instance;

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
    
    public InputAction movementAction;

    private void Start()
    {
        movementAction = InputSystem.actions["Move"];
    }
}
