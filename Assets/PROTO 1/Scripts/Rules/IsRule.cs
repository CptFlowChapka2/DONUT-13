using System;
using System.Collections.Generic;
using UnityEngine;

public class IsRule : KeywordRule
{
    public void Evaluate(int selfPose, List<Slot> slots, StateUpdater su)
    {
        switch (slots[selfPose+1].slottedKeyWord.type)
        {
            case KeyWordType.Object:
                EvaluateToObject(slots[selfPose-1].slottedKeyWord,slots[selfPose+1].slottedKeyWord,su);
                break;
            case KeyWordType.Parameter:
                EvaluateToParam(slots[selfPose-1].slottedKeyWord,slots[selfPose+1].slottedKeyWord,su);
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
                throw new NotImplementedException();
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
                su.UpdateParamField(left.IdentityName,right.paramValuePair.Item1,right.paramValuePair.Item2);
                break;
            case KeyWordType.Player:
                throw new NotImplementedException();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
