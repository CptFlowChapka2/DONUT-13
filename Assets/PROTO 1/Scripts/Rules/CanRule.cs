using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class CanRule : KeywordRule
{
    public override void Evaluate(int selfPose, List<Slot> slots,StateUpdater su)
    {
        if(slots[selfPose-1].slottedKeyWord.type==KeyWordType.Player)
        {
            su.AttributeFunctionalityToPlayer(slots[selfPose+1].slottedKeyWord.functionality);
            if (slots[selfPose + 1].slottedKeyWord.slotRestriction.authorizedRight.Length != 1)
            {
                su.AttributeActableIdendityToPlayer(slots[selfPose+1].slottedKeyWord.functionality, slots[selfPose+2].slottedKeyWord.IdentityName);
            }
            return;
        }
        
        su.UpdateIdentityFonctionality(slots[selfPose-1].slottedKeyWord.IdentityName,slots[selfPose+1].slottedKeyWord.functionality);
        if (slots[selfPose + 1].slottedKeyWord.slotRestriction.authorizedRight.Length != 1)
        {
            su.UpdateActableIdentity(slots[selfPose-1].slottedKeyWord.IdentityName,slots[selfPose+1].slottedKeyWord.functionality,
                slots[selfPose+2].slottedKeyWord.IdentityName);
        }
         
    }
}

public abstract class  KeywordRule : MonoBehaviour
{
    public abstract void Evaluate(int selfPose,List<Slot> slots,StateUpdater su);
}





