using UnityEngine;

[CreateAssetMenu(fileName = "HolderOfSingleTon", menuName = "Scriptable Objects/HolderOfSingleTon")]
public class HolderOfSingleTon : ScriptableObject
{
    public InputManager inputManager;
    public EntityDatabase entityDatabase;
    public StateUpdater stateUpdater;
}
