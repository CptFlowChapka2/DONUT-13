using System;
using System.Collections.Generic;
using UnityEngine;

public class RulesManager : MonoBehaviour
{
    public GameObject rulePrefab;
    public GameObject inventorySlotsPrefab;
    public FunctionalityAffectationTable fat;
    public HolderOfSingleTon HolderOfSingleTon;
    public Rule[] allRules;
    public AllInventoryKeyWords inventoryKeywords;
    
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
            rule.CreateCuratedRestriction();
            rule.Evaluate(HolderOfSingleTon.stateUpdater);
        }
        HolderOfSingleTon.stateUpdater.UpdateTruth();
    }
}
