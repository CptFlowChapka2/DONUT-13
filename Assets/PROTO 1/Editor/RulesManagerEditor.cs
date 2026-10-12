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
        
        GUI.enabled = false;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("allRules"));
        GUI.enabled = true;

        if (GUILayout.Button("Update Rules UI"))
        {
            Undo.RecordObject(instance, "Update Rules UI");
            OnUpdateInEditor(instance);
        }
        
        //Add your code
        serializedObject.ApplyModifiedProperties();
    }
    
    public void OnUpdateInEditor(RulesManager instance)
    {
        //Clear anciennes rules
        for (int i = 0; i < instance.allRules.Length; i++)
        {
            DestroyImmediate(instance.allRules[i].gameObject);
        }
        
        instance.allRules = new Rule[instance.fat.nbRules];
        
        GameObject canvas = EditorSceneManager.GetActiveScene().GetRootGameObjects().ToList()
            .Find(x => x.CompareTag("Canvas"));
        
        List<KeyWord> keywords = canvas.GetComponentsInChildren<KeyWord>().ToList();
        KeyWord emptyKW = keywords.Find(x => x.type == KeyWordType.None);

        //Clear anciens keywords
        for (int i = 0; i < keywords.Count; i++)
        {
            if (keywords[i] != emptyKW)
            {
                DestroyImmediate(keywords[i].gameObject);
            }
        }
        
        //Clear inventaire
        Inventory inv = canvas.GetComponentInChildren<Inventory>();
        DestroyImmediate(inv.gameObject);
        
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
        GameObject inventorySlotsObject = Instantiate(instance.inventorySlotsPrefab, canvas.transform, false);
        Inventory inventory = inventorySlotsObject.GetComponent<Inventory>();
        
        for (int i = 0; i < inventory.inventorySlots.Count; i++)
        {
            GameObject kw = Instantiate(instance.inventoryKeywords.inventoryKeyWordsPrefabs[i], canvas.transform, false);
            Slot concernedSlot = inventory.inventorySlots[i];
            concernedSlot.slottedKeyWord = kw.GetComponent<KeyWord>();
            concernedSlot.slottedKeyWordTransform = kw.GetComponent<RectTransform>();
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
}