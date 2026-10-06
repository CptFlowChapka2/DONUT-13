using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rule : MonoBehaviour
{
    public List<Slot> slots;

    public void Evaluate(StateUpdater su)
    {
        Slot verb = slots.Find(x => x.slottedKeyWord.type == KeyWordType.Verb);
        int i = slots.FindIndex(x=>x.Equals(verb)); 
        verb.slottedKeyWord.verb.Evaluate(i, slots, su);
       
        
    }
}
