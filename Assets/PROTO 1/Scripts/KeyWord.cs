using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum KeyWordType
{
    None,
    Object,
    Parameter,
    Fonctionality,
    Verb,
    Player
}

[Serializable]
public class KeyWord : MonoBehaviour
{
    
    public KeyWordType type;
    public IdentityName IdentityName;
    public KeywordRule verb;
    public ModifySlotRestriction slotRestriction;
    public (string, object) paramValuePair;
    public SerializableType<Functionality> functionality;
    
}

[Serializable]
public struct ModifySlotRestriction
{
    public KeyWordType[] authorizedleft;
    public KeyWordType[] authorizedRight;
}
