using System.Collections.Generic;
using UnityEngine;

public class StateUpdater : MonoBehaviour
{
    [SerializeField]public Dictionary<SerializableType<Fonctionality>,Dictionary<Identity,bool>> AllMightyDictionary;

    private void UpdateAllEntities()
    {
        foreach (var foncDicPair in AllMightyDictionary)
        {
            foreach (var identityActivationState in foncDicPair.Value)
            {
                //CRIME
                identityActivationState.Key.linkedEntities.ForEach(x=>x.SetComponentEnable(foncDicPair.Key.type,identityActivationState.Value));
            }
        }
    }
}