using System;
using System.Collections.Generic;
using UnityEngine;

public class IsRule : KeywordRule
{
    public override void Evaluate(int selfPosInRule, List<Slot> slots, StateUpdater su)
    {
        switch (slots[selfPosInRule+1].slottedKeyWord.type)
        {
            case KeyWordType.Object:
                EvaluateToObject(slots[selfPosInRule-1].slottedKeyWord,slots[selfPosInRule+1].slottedKeyWord,su);
                break;
            case KeyWordType.Parameter:
                EvaluateToParam(slots[selfPosInRule-1].slottedKeyWord,slots[selfPosInRule+1].slottedKeyWord,su);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }

    private void EvaluateToObject(KeyWord left, KeyWord right, StateUpdater su)
    {
        switch (left.type)
        {
            case KeyWordType.Object:
                su.TransformEntitiesToNewIdentity(left.IdentityName,right.IdentityName);
                break;
            case KeyWordType.Player:
               su.AttributeIdentityToPlayer(right.IdentityName);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void EvaluateToParam(KeyWord left, KeyWord right,StateUpdater su)
    {
        switch (left.type)
        {
            case KeyWordType.Object:
                su.UpdateParamField(left.IdentityName,right.paramValuePair.name,right.paramValuePair.value);
                break;
            case KeyWordType.Player:
                throw new NotImplementedException();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
