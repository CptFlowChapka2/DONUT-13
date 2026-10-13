using System;
using System.Collections.Generic;
using UnityEngine;

public class RulesManager : MonoBehaviour
{
    public GameObject rulePrefab;
    public GameObject inventorySlotsPrefab;
    public GameObject rulePagePrefab;
    public FunctionalityAffectationTable fat;
    public HolderOfSingleTon HolderOfSingleTon;
    public Rule[] allRules;
    public AllInventoryKeyWords inventoryKeywords;
    public List<GameObject> inventoryPages = new List<GameObject>();
    public List<GameObject> rulePages = new List<GameObject>();
    [SerializeField] private GameObject canvas;
    
    private bool isClosed = false;
    
    private int activeInventoryPage = 0;
    private int activeRulePage = 0;
    
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

    public void AdvanceInInventory(int page)
    {
        int p = activeInventoryPage + page;
        if (p == 3)
        {
            foreach (GameObject invPage in inventoryPages)
            {
                invPage.GetComponent<RectTransform>().anchoredPosition += new Vector2(page * 4000f, 0);
                foreach (Slot slot in invPage.GetComponentsInChildren<Slot>())
                {
                    slot.anchorPoint += new Vector2(4000f, 0);
                }
            }
            activeInventoryPage = 0;
        }
        else if (p == -1)
        {
            foreach (GameObject invPage in inventoryPages)
            {
                invPage.GetComponent<RectTransform>().anchoredPosition += new Vector2(-4000f, 0);
                foreach (Slot slot in invPage.GetComponentsInChildren<Slot>())
                {
                    slot.anchorPoint += new Vector2(-4000f, 0);
                }
            }
            activeInventoryPage = inventoryPages.Count - 1;
        }
        else
        {
            foreach (GameObject invPage in inventoryPages)
            {
                invPage.GetComponent<RectTransform>().anchoredPosition += new Vector2(-page * 2000f, 0);
                foreach (Slot slot in invPage.GetComponentsInChildren<Slot>())
                {
                    slot.anchorPoint += new Vector2(-page * 2000f, 0);
                }
            }
            activeInventoryPage = p;
        }
    }

    public void CloseCanvas()
    {
        if (!isClosed)
        {
            canvas.SetActive(false);
            isClosed = true;
        }
        else
        {
            canvas.SetActive(true);
            isClosed = false;
        }
    }
    
    public void AdvanceInRules(int page)
    {
        int p = activeRulePage + page;
        if (rulePages.Count == p)
        {
            foreach (GameObject rPage in rulePages)
            {
                rPage.GetComponent<RectTransform>().anchoredPosition += new Vector2(2000f * (rulePages.Count - 1), 0);
                foreach (Slot slot in rPage.GetComponentsInChildren<Slot>())
                {
                    slot.anchorPoint += new Vector2(2000f * (rulePages.Count - 1), 0);
                }
            }
            activeRulePage = 0;
        }
        else if (p == -1)
        {
            foreach (GameObject rPage in rulePages)
            {
                rPage.GetComponent<RectTransform>().anchoredPosition += new Vector2(-2000f * (rulePages.Count - 1), 0);
                foreach (Slot slot in rPage.GetComponentsInChildren<Slot>())
                {
                    slot.anchorPoint += new Vector2(-2000f * (rulePages.Count - 1), 0);
                }
            }
            activeRulePage = rulePages.Count - 1;
        }
        else
        {
            foreach (GameObject rPage in rulePages)
            {
                rPage.GetComponent<RectTransform>().anchoredPosition += new Vector2(-page * 2000f, 0);
                foreach (Slot slot in rPage.GetComponentsInChildren<Slot>())
                {
                    slot.anchorPoint += new Vector2(-page * 2000f, 0);
                }
            }
            activeRulePage = p;
        }
    }
}
