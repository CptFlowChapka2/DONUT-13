using System;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public HolderOfSingleTon holderOfSingleTon;
    public Vector3 inputsThisFrame;

    private void Awake()
    {
        holderOfSingleTon.inputManager = this;
    }

    void Update()
    {
        inputsThisFrame = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) inputsThisFrame += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) inputsThisFrame += Vector3.back;
        if (Input.GetKey(KeyCode.A)) inputsThisFrame += Vector3.left;
        if (Input.GetKey(KeyCode.D)) inputsThisFrame += Vector3.right;

        inputsThisFrame = inputsThisFrame.normalized;
    }
}
