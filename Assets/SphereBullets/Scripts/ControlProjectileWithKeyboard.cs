using System;
using UnityEngine;

public class ControlProjectileWithKeyboard : MonoBehaviour
{
    private BasicProjectile projectileToControl;
    
    private void Awake()
    {
        projectileToControl = GetComponent<BasicProjectile>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            projectileToControl.rotationSpeed = -5f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            projectileToControl.rotationSpeed = 5f;
        }
        else
        {
            projectileToControl.rotationSpeed = 0f;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            projectileToControl.speed = 0f;
        }
        else
        {
            projectileToControl.speed = projectileToControl.projectileData.speed;
        }
    }
}
