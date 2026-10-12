using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rule : MonoBehaviour
{
    public List<Slot> slots;
    [SerializeField] public List<Vector2> slotsAnchors;
    
    public void Evaluate(StateUpdater su)
    {
        if (slots.Any(x => !x.curatedAuthorizedTypes.Contains(x.slottedKeyWord.type)))
        {
            Debug.Log("A Rule was not able to Evaluate , there was a type mismatch=");
            return;
        }
        if (slots.Any(x => (x.slottedKeyWord.type == KeyWordType.None && !x.curatedAuthorizedTypes.Contains(KeyWordType.None))))
        {
            
            Debug.Log("A Rule was not able to Evaluate , either because it had a invalid empty slot");
            return;
        }
        
        Slot verb = slots.Find(x => x.slottedKeyWord.type == KeyWordType.Verb);
        if (verb is null)
        {
            Debug.Log("A Rule was not able to Evaluate ,because it had no VERB");
            return;
        }
        int i = slots.FindIndex(x=>x.Equals(verb)); 
        verb.slottedKeyWord.verb.Evaluate(i, slots, su);
       
        
    }

    public void CreateCuratedRestriction()
    {
        if (slots[1].slottedKeyWord.type != KeyWordType.None)
        {
            slots[1].curatedAuthorizedTypes = slots[1].authorizedType;
            slots[0].curatedAuthorizedTypes =
            slots[0].authorizedType.Intersect(slots[1].slottedKeyWord.slotRestriction.authorizedleft).ToArray();
        slots[2].curatedAuthorizedTypes =
            slots[2].authorizedType.Intersect(slots[1].slottedKeyWord.slotRestriction.authorizedRight).ToArray();
        }
        if(slots[2].slottedKeyWord.type!=KeyWordType.None)
        {
            slots[3].curatedAuthorizedTypes =
                slots[3].authorizedType.Intersect(slots[2].slottedKeyWord.slotRestriction.authorizedRight).ToArray();
        }
        
    }
}
