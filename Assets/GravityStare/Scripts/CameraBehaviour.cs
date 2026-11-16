using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private float maxDistanceToAvatar = 10.0f;
    [SerializeField] private float minDistanceToAvatar = 1.0f;
    [SerializeField] private float sensibilityZoom = 1f;
    [SerializeField] private float sensibilityRotation = 1f;
    private float distanceToAvatar = 8.0f;
    
    private Player player;
    private StableAnchor stableAnchor;
    
    private Vector3 relativePosition = -Vector3.forward;
    
    private void Start()
    {
        player = Player.instance;
        stableAnchor = StableAnchor.instance;
        transform.position = stableAnchor.transform.position + (relativePosition * distanceToAvatar);
        transform.LookAt(stableAnchor.transform.position);
    }
    
    private void Update()
    {
        Vector3 deltaMousePosition = Input.mousePositionDelta;
        Quaternion rotX = Quaternion.AngleAxis(deltaMousePosition.x * sensibilityRotation, transform.up);
        Quaternion rotY = Quaternion.AngleAxis(-deltaMousePosition.y * sensibilityRotation, transform.right);
        
        relativePosition = rotX * relativePosition;
        relativePosition = rotY * relativePosition;
    
        relativePosition = new Vector3(relativePosition.x, Mathf.Clamp(relativePosition.y, -0.70f, 0.70f),
            relativePosition.z);
    
        if (Physics.Raycast(stableAnchor.transform.position, relativePosition, out RaycastHit hit, maxDistanceToAvatar,
                layerMask: 3))
        {
            distanceToAvatar = hit.distance;
            distanceToAvatar = Mathf.Clamp(distanceToAvatar, minDistanceToAvatar, maxDistanceToAvatar);
        }
        else
        {
            distanceToAvatar = maxDistanceToAvatar;
        }
        
    
        transform.position = stableAnchor.transform.position + (relativePosition.normalized * distanceToAvatar);
    
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
    
        RecalculateUp();
    }
    
    private void RecalculateUp()
    {
        Vector3 dirToUp = Vector3.ProjectOnPlane(transform.up, -relativePosition).normalized;
        if (dirToUp.sqrMagnitude <= 0.0001f) return;
        transform.rotation = Quaternion.LookRotation(-relativePosition, dirToUp);
        //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
    }
}