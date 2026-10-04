using System.Collections.Generic;
using UnityEngine;

public abstract class Verb : MonoBehaviour
{ 
    public abstract void AddValues(ref Dictionary<string, object> dic);
    public abstract void RemoveValues(ref Dictionary<string, object> dic);
    public abstract void ReplaceValues(ref Dictionary<string, object> dic);
    public abstract void UpdateValues(ref Dictionary<string, object> dic);

    public static string CreateParamString<T>( object param) where T : Verb
    {
        string re = nameof(T) + "_" + nameof(param);
        Debug.Log(re);
        return re;
    }
}
