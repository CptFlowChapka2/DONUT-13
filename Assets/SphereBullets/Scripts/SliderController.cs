using UnityEngine;

public class SliderController : MonoBehaviour
{
    public Projectile toControl;

    public void OnValueChanged(float value)
    {
        toControl.rotationSpeed = value;
    }
}
