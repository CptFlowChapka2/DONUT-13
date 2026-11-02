using UnityEngine;

public class SliderController : MonoBehaviour
{
    public Projectile toControl;

    public void OnValueChanged(float value)
    {
        toControl.projectileData.rotationSpeed = value;
    }
}
