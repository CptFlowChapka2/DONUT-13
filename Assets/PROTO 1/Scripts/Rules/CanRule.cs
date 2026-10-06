using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class CanRule : KeywordRule
{
    public void Evaluate(int selfPose, List<Slot> slots,StateUpdater su)
    {
        su.UpdateIdentityFonctionality(slots[selfPose-1].slottedKeyWord.IdentityName,slots[selfPose+1].slottedKeyWord.functionality);
        if (slots[selfPose + 1].slottedKeyWord.slotRestriction.authorizedRight.Length != 1)
        {
            su.UpdateActableIdentity(slots[selfPose-1].slottedKeyWord.IdentityName,slots[selfPose+1].slottedKeyWord.functionality,
                slots[selfPose+2].slottedKeyWord.IdentityName);
        }
         
    }
}

public interface  KeywordRule
{
    public void Evaluate(int selfPose,List<Slot> slots,StateUpdater su);
}





