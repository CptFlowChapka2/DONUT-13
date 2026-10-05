using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StateUpdater : MonoBehaviour
{
    [SerializeField] public Dictionary<Identity, Dictionary<SerializableType<Functionality>,bool>> allMightyDictionary;

    private void UpdateAllEntities()
    {
        foreach (var IdDicPair in allMightyDictionary)
        {
            foreach (var identityActivationState in IdDicPair.Value)
            {
                //CRIME
                IdDicPair.Key.linkedEntities.ForEach(x=>x.SetComponentEnable(identityActivationState.Key.type,identityActivationState.Value));
            }
        }
    }
}

