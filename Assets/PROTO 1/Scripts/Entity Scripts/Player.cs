using System;
using System.Collections.Generic;
using UnityEngine;


public class Player : Verb
{
    public Transform parent;

    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            parent.position += Vector3.right;
        }
    }

    public override void AddValues(ref Dictionary<string, object> dic)
    {
        dic.TryAdd(CreateParamString<Player>(parent), GetComponentInParent<Transform>());
        ReplaceValues(ref dic);
    }

    public override void RemoveValues(ref Dictionary<string, object> dic)
    {
        dic.Remove(CreateParamString<Player>(parent));
    }

    public override void ReplaceValues(ref Dictionary<string, object> dic)
    {
        parent = (Transform)dic[CreateParamString<Player>(parent)];
    }

    public override void UpdateValues(ref Dictionary<string, object> dic)
    {
        dic[CreateParamString<Player>(parent)] = parent;
    }
}
