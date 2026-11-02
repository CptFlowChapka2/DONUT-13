using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Projectile toControl;

    public void OnValueChanged(float value)
    {
        toControl.rotationSpeed = value;
    }
}
