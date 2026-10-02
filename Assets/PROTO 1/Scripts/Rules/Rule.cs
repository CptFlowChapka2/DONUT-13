using UnityEngine;

public abstract class Rule : MonoBehaviour
{
    public Slot[] slots;

    public abstract void Evaluate();
}
