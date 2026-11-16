using System;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    #region Singleton

    public static CameraFocus instance;

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
    }

    #endregion
    
    private Player player;
    private Rigidbody rigidbody;

    private void Start()
    {
        player = Player.instance;
        rigidbody = player.GetComponent<Rigidbody>();
        transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;
    }

    private void Update()
    {
        transform.position = player.transform.position;
        
        switch (player.playerState)
        {
            case PlayerState.InSpace:
                transform.forward = rigidbody.linearVelocity.normalized;
                break;
            case PlayerState.Grounded:
                transform.forward = player.transform.forward;
                transform.up = player.transform.up;
                break;
            default:
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.up);
    }
}
