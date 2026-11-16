using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraBehaviour : MonoBehaviour
{
    private Player player;
    private CameraFocus cameraFocus;
    
    [SerializeField] private float maxDistanceToAvatar = 20.0f;
    [SerializeField] private float minDistanceToAvatar = 12.0f;
    [SerializeField] private float sensibilityZoom = 1f;
    [SerializeField] private float sensibilityRotation = 1f;
    private float distanceToAvatar = 8.0f;

    private Vector3 relativePosition = -Vector3.forward;

    private void Start()
    {
        player = Player.instance;
        cameraFocus = CameraFocus.instance;
        transform.position = cameraFocus.transform.position + (relativePosition * distanceToAvatar);
        transform.LookAt(cameraFocus.transform.position);
    }

    private void Update()
    {
        Vector3 deltaMousePosition = Input.mousePositionDelta;
        Quaternion rotX = Quaternion.AngleAxis(deltaMousePosition.x * sensibilityRotation, Vector3.up);
        Quaternion rotY = Quaternion.AngleAxis(-deltaMousePosition.y * sensibilityRotation, Vector3.right);
        relativePosition = rotX * rotY * relativePosition;
        
        transform.position = cameraFocus.transform.position + (relativePosition * distanceToAvatar);
        
        switch (player.playerState)
        {
            case PlayerState.InSpace:
                break;
            case PlayerState.Grounded:
                //Au mouvement de la souris:
                  //Droite/Gauche/Haut/Bas: déplace la cam en orbite autour du camFocus
                break;
            default:
                break;
        }
        
        transform.LookAt(cameraFocus.transform.position);
        Zoom();
    }

    private Vector2 previousPosition = Vector2.zero;

    // private void Focus()
    // {
    //     camera.transform.position = focus.transform.position;
    //     camera.transform.forward = -focus.transform.position.normalized;
    //     camera.transform.up = focus.transform.forward;
    // }
    
    private void Move()
    {
        if (Input.GetMouseButtonDown(2) || Input.GetMouseButtonDown(1))
        {
            previousPosition = Input.mousePosition;
            return;
        }
        if (Input.GetMouseButtonUp(2) || Input.GetMouseButtonUp(1))
        {
            previousPosition = Vector2.zero;
            return;
        }

        if (Input.GetMouseButton(2) || Input.GetMouseButton(1))
        {
            Vector2 nextPosition = Input.mousePosition;
            Vector2 delta = nextPosition - previousPosition;
            transform.Rotate(Vector3.up, delta.x * sensibilityRotation);
            transform.Rotate(Vector3.right, -delta.y * sensibilityRotation);
            previousPosition = nextPosition;
        }
    }
    
    private void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            distanceToAvatar -= Input.GetAxis("Mouse ScrollWheel") * sensibilityZoom;
            distanceToAvatar = Mathf.Clamp(distanceToAvatar, minDistanceToAvatar, maxDistanceToAvatar);
        }
        //camera.transform.position = transform.position + (camera.transform.position - transform.position).normalized * distanceToAvatar;
    }
}