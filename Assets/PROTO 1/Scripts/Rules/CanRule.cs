using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CanRule : Rule
{
    public override void Evaluate()
    {
        if(slots[0].slottedKeyWord.type==KeyWordType.None || slots[1].slottedKeyWord.type==KeyWordType.None) return;
        switch (slots[0].slottedKeyWord.type,slots[1].slottedKeyWord.type)
        {
            case (KeyWordType.Object,KeyWordType.Verb) :
                slots[1].slottedKeyWord.referencedObject.gameObject.AddComponent(slots[0].slottedKeyWord.test.type);
                break;
            
        }
    }
}





