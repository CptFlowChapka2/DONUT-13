using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class CanRule : KeywordRule
{
    public override void Evaluate(int selfPosInRule, List<Slot> slots, StateUpdater su)
    {
        if(slots[selfPosInRule-1].slottedKeyWord.type==KeyWordType.Player)
        {
            su.AttributeFunctionalityToPlayer(slots[selfPosInRule+1].slottedKeyWord.functionality);
            if (slots[selfPosInRule + 1].slottedKeyWord.slotRestriction.authorizedRight.Length != 1)
            {
                su.AttributeActableIdendityToPlayer(slots[selfPosInRule+1].slottedKeyWord.functionality, slots[selfPosInRule+2].slottedKeyWord.IdentityName);
            }
            return;
        }
        
        su.UpdateIdentityFonctionality(slots[selfPosInRule-1].slottedKeyWord.IdentityName,slots[selfPosInRule+1].slottedKeyWord.functionality);
        if (slots[selfPosInRule + 1].slottedKeyWord.slotRestriction.authorizedRight.Length != 1)
        {
            su.UpdateActableIdentity(slots[selfPosInRule-1].slottedKeyWord.IdentityName,slots[selfPosInRule+1].slottedKeyWord.functionality,
                slots[selfPosInRule+2].slottedKeyWord.IdentityName);
        }
         
    }
}

public abstract class  KeywordRule : MonoBehaviour
{
    public abstract void Evaluate(int selfPosInRule,List<Slot> slots,StateUpdater su);
}





