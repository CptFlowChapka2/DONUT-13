using System;
using UnityEngine;


public class Player : Verb
{
    public Transform parent;

    private void Start()
    {
        parent = GetComponentInParent<Transform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            parent.position += Vector3.right;
        }
    }

    public override void ReplaceValue(Verb verb)
    {
        if ((verb as Player) == null) throw new NullReferenceException("tried Replacing verb Player with a non Player Verb");
        parent = (verb as Player).parent;
    }
}
