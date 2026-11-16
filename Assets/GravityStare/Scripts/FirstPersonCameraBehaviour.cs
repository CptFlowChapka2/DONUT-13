using System;
using UnityEngine;

public class FirstPersonCameraBehaviour : MonoBehaviour
{
    [SerializeField] private float sensibilityRotation = 1f;
    [SerializeField] private GameObject target;
    private void FixedUpdate()
    {
        transform.position = target.transform.position;
    }

    private void Update()
    {
        
        Vector3 deltaMousePosition = Input.mousePositionDelta;
        Quaternion rotX = Quaternion.AngleAxis(deltaMousePosition.x * sensibilityRotation, transform.up);
        Quaternion rotY = Quaternion.AngleAxis(-deltaMousePosition.y * sensibilityRotation, transform.right);

        
        var vec = rotY * transform.forward;
        Player.instance.transform.forward = rotX*Player.instance.transform.forward;
        if(Vector3.Distance(vec,target.transform.up)<=0.01f) return;
        transform.rotation = Quaternion.LookRotation(vec,target.transform.up);
    }
}
