using UnityEngine;

public class CameraFocusBehaviour : MonoBehaviour
{
    private StableAnchor anchor;
    [SerializeField] private Camera cam;
    
    [SerializeField] private float maxDistanceToAvatar = 10.0f;
    [SerializeField] private float minDistanceToAvatar = 1.0f;
    [SerializeField] private float sensibilityZoom = 1f;
    [SerializeField] private float sensibilityRotation = 1f;
    private float distanceToAvatar = 8.0f;
    
    private float horizontalRotation;
    private float verticalRotation;

    private Quaternion initialRotation;

    private void Start()
    {
        anchor = StableAnchor.instance;
        transform.position = anchor.transform.position;
        transform.rotation = anchor.transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        transform.position = anchor.transform.position;
        RotateByMouse();
        cam.transform.rotation = transform.rotation;
        cam.transform.position = transform.position + -transform.forward * distanceToAvatar;
    }

    private void RotateByMouse()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        horizontalRotation += mouseX;
        verticalRotation -= mouseY;

        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        Quaternion horizontal = Quaternion.AngleAxis(horizontalRotation, Vector3.up);
        Quaternion vertical = Quaternion.AngleAxis(verticalRotation, Vector3.right);

        transform.rotation = anchor.transform.rotation * horizontal * vertical;
    }
}
