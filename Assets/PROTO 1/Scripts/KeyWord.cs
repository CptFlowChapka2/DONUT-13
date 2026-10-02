using System.Collections.Generic;
using UnityEngine;

public enum KeyWordType
{
    None,
    Object,
    Parameter,
    Verb,
    Control
}

public class KeyWord : MonoBehaviour
{
    public KeyWordType type;
    public List<GameObject> referencedObjects;
}
