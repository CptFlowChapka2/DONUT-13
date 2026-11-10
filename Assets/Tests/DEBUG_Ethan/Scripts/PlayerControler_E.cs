using UnityEngine;

public class PlayerControler_E : MonoBehaviour
{
    public float speed;
    public float rotSpeed;
    public Vector2 inputDir;
    public Vector3 movementDir;
    public Transform mesh;
    
    void Update()
    {
        GetMovementDir();
        
        if (inputDir != Vector2.zero)
            transform.position += transform.forward * (Time.deltaTime * speed);
        
        
    }

    void GetMovementDir()
    {
        inputDir.x = Input.GetAxisRaw("Horizontal");
        inputDir.y = Input.GetAxisRaw("Vertical");

        movementDir = new Vector3(inputDir.x, 0f, inputDir.y);
        
        if (inputDir != Vector2.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, movementDir, rotSpeed * Time.deltaTime);
        }
    }
}
