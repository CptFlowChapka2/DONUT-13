using System.Collections.Generic;
using System.Linq;
using Unity.Hierarchy;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(RulesManager))]
public class RulesManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        RulesManager instance = (RulesManager)target;
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rulePrefab"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("inventorySlotsPrefab"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("fat"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("HolderOfSingleTon"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("inventoryKeywords"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("inventoryPages"));
        
        GUI.enabled = false;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("allRules"));
        GUI.enabled = true;

        if (GUILayout.Button("Update Rules UI"))
        {
            Undo.RecordObject(instance, "Update Rules UI");
            OnUpdateInEditor(instance);
        }
        
        serializedObject.ApplyModifiedProperties();
    }
    
    public void OnUpdateInEditor(RulesManager instance)
    {
        GameObject canvas = EditorSceneManager.GetActiveScene().GetRootGameObjects().ToList()
            .Find(x => x.CompareTag("Canvas"));
        List<KeyWord> keywords = canvas.GetComponentsInChildren<KeyWord>().ToList();
        KeyWord emptyKW = keywords.Find(x => x.type == KeyWordType.None);
        
        //Clear anciennes rules
        if (instance.allRules.Length != 0)
        {
            for (int i = 0; i < instance.allRules.Length; i++)
            {
                if (instance.allRules[i] == null) continue;
                DestroyImmediate(instance.allRules[i].gameObject);
            }
        }
        
        instance.allRules = new Rule[instance.fat.nbRules];
        
        //Clear anciens keywords
        if (keywords.Count != 0)
        {
            for (int i = 0; i < keywords.Count; i++)
            {
                if (keywords[i].gameObject == null) continue;
                if (keywords[i] != emptyKW)
                {
                    DestroyImmediate(keywords[i].gameObject);
                }
            }
        }
        
        //Clear inventaire
        if (instance.inventoryPages.Count != 0)
        {
            for (int i = 0; i < instance.inventoryPages.Count; i++)
            {
                if (instance.inventoryPages[i].gameObject == null) continue;
                instance.inventoryPages.RemoveAt(i);
                DestroyImmediate(instance.inventoryPages[i]);
            }
            instance.inventoryPages = new List<GameObject>();
        }
        
        //Création de Règle
        for (int i = 0; i < instance.fat.nbRules; i++)
        {
            GameObject ruleObject = Instantiate(instance.rulePrefab, canvas.transform, false);
            
            ruleObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 384 - i * 100);
            
            Rule rule = ruleObject.GetComponent<Rule>();
            for (int j = 0; j < rule.slots.Count; j++)
            {
                rule.slots[j].slottedKeyWord = emptyKW;
                rule.slots[j].anchorPoint = new Vector2(0, 384 - i * 100) + rule.slotsAnchors[j];
            }
            
            instance.allRules[i] = rule;
        }
        
        //Création d'Inventaire
        for (int i = 0; i < 3; i++)
        {
            CreateInventoryPage(instance, canvas, emptyKW, i);
            instance.inventoryPages[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(i * 2000f, -262.5f);
        }
        
        //Création de KW
        foreach (KeyWordInRule kw in instance.fat.keyWords)
        {
            GameObject kwObject = Instantiate(kw.keyWordPrefab, canvas.transform, false);
            Slot concernedSlot = instance.allRules[kw.posInRule.x].slots[kw.posInRule.y];
            concernedSlot.slottedKeyWord = kwObject.GetComponent<KeyWord>();
            concernedSlot.slottedKeyWordTransform = kwObject.GetComponent<RectTransform>();
        }
    }

    private static void CreateInventoryPage(RulesManager instance, GameObject canvas, KeyWord emptyKW, int pageNumber)
    {
        GameObject inventorySlotsObject = Instantiate(instance.inventorySlotsPrefab, canvas.transform, false);
        instance.inventoryPages.Add(inventorySlotsObject);
        Inventory inventory = inventorySlotsObject.GetComponent<Inventory>();
        
        for (int i = 8 * pageNumber; i < inventory.inventorySlots.Count + (8 * pageNumber); i++)
        {
            Debug.Log(i);
            if (i >= instance.inventoryKeywords.inventoryKeyWordsPrefabs.Count)
            {
                Slot emptySlot = inventory.inventorySlots[i - (8 * pageNumber)];
                emptySlot.slottedKeyWord = emptyKW;
                continue;
            }
            GameObject kw = Instantiate(instance.inventoryKeywords.inventoryKeyWordsPrefabs[i], canvas.transform, false);
            Slot concernedSlot = inventory.inventorySlots[i - (8 * pageNumber)];
            concernedSlot.slottedKeyWord = kw.GetComponent<KeyWord>();
            concernedSlot.slottedKeyWordTransform = kw.GetComponent<RectTransform>();
        }
    }
}