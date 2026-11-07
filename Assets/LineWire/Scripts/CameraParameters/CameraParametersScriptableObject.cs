using UnityEngine;

[CreateAssetMenu(fileName = "CameraParametersScriptableObject", menuName = "Scriptable Objects/CameraParametersScriptableObject")]
public class CameraParametersScriptableObject : ScriptableObject
{
    public float fov;
    public float cameraDistance;
    public float cameraHeight;
    public float cameraXRotation;
}
