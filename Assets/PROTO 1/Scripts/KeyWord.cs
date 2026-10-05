using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum KeyWordType
{
    None,
    Object,
    Parameter,
    Verb,
    Control
}

[Serializable]
public class KeyWord : MonoBehaviour
{
    public string name;
    public KeyWordType type;
    public GameObject referencedObject;
    public (string, object) paramValuePair;
    public SerializableType<Verb> test;

    private void Start()
    {
        if(referencedObject==null) return;
        referencedObject.GetComponent<DefaultPreFabValues>().actualKeyword = this;
    }
}
