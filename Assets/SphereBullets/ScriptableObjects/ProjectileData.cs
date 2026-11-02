using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "Scriptable Objects/ProjectileData")]
public class ProjectileData : ScriptableObject
{
    [Header("Velocity")]
    public float speed;
    public float rotationSpeed;
    [Header("Bounciness")]
    public float heightBounciness;
    public float lengthBounciness;
}
