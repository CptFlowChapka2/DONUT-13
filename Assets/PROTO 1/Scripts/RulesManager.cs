using System;
using UnityEngine;

public class RulesManager : MonoBehaviour
{
    public HolderOfSingleTon HolderOfSingleTon;
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
        HolderOfSingleTon.stateUpdater.ClearTruth();

        foreach (var rule in allRules)
        {
            rule.Evaluate(HolderOfSingleTon.stateUpdater);
        }
    }
}
