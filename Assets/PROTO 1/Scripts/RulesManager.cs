using System;
using UnityEngine;

public class RulesManager : MonoBehaviour
{
    public Rule[] allRules;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("NE PAS SPAMMER PAR PITIER C'EST PAS OPTI");
            EvaluateAll();
        }
    }

    public void EvaluateAll()
    {
        foreach (var allRule in allRules)
        {
          allRule.Evaluate();  
        }

        var holder = FindAnyObjectByType<EntityPrefabHolder>();
        
        holder.allEntities.ForEach(x=>
            x.ReplaceIdentity(x.currentKeyword.referencedObject)
        );
    }
}
