using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isGrounded;
    public ForceReceiver thisForceReceiver;
    public Camera camera;
    private TagHandle handle;
    
    private void Start()
    {
        handle=TagHandle.GetExistingTag("Ground");
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        Debug.DrawRay(camera.transform.position,camera.transform.forward,Color.blueViolet);
        if (Input.GetMouseButtonDown(0))
        {
            
            RaycastHit[] hits = new RaycastHit[10];
            for (int i = 0; i < Physics.SphereCastNonAlloc(camera.transform.position,8,camera.transform.forward, hits); i++)
            {
                var raycastHit = hits[i];
                if (!raycastHit.collider.CompareTag(handle)) continue;
                raycastHit.transform.gameObject.GetComponentInChildren<ForceApplier>().AddOrToggleReceiver(thisForceReceiver,true);
                break;
            }
        }
        
    }
}
