using System.Collections.Generic;
using UnityEngine;

public abstract class Verb : MonoBehaviour
{ 
    public abstract void AddValues(ref Dictionary<string, object> dic);
    public abstract void RemoveValues(ref Dictionary<string, object> dic);
    public abstract void ReplaceValues(ref Dictionary<string, object> dic);
    public abstract void UpdateValues(ref Dictionary<string, object> dic);
    
}
