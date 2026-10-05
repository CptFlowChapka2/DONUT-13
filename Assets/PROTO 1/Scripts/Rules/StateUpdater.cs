using System.Collections.Generic;
using UnityEngine;

public class StateUpdater : MonoBehaviour
{
    [SerializeField] private Identity[] allIdentities;
    
    [SerializeField] public Dictionary<Identity, Dictionary<SerializableType<Functionality>,bool>> allMightyDictionary;

    private void UpdateAllEntities()
    {
        foreach (var IdDicPair in allMightyDictionary)
        {
            foreach (var identityActivationState in IdDicPair.Key)
            {
                //CRIME
                identityActivationState.Key.linkedEntities.ForEach(x=>x.SetComponentEnable(IdDicPair.Key.type,identityActivationState.Value));
            }
        }
    }
}