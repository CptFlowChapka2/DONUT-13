using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    
    public Vector3 inputsThisFrame;
    // Update is called once per frame
    void Update()
    {
        inputsThisFrame = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W)) inputsThisFrame += Vector3.forward;
        if (Input.GetKeyDown(KeyCode.S)) inputsThisFrame += Vector3.back;
        if (Input.GetKeyDown(KeyCode.A)) inputsThisFrame += Vector3.left;
        if (Input.GetKeyDown(KeyCode.D)) inputsThisFrame += Vector3.right;

        
    }
}
