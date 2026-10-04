using System;
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

[Serializable]
public class KeyWord : MonoBehaviour
{
    public string name;
    public KeyWordType type;
    public GameObject referencedObject;
    public (string, object) paramValuePair;

    private void Start()
    {
        if(referencedObject==null) return;
        referencedObject.GetComponent<DefaultPreFabValues>().actualKeyword = this;
    }
}
