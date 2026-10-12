using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FunctionalityAffectationTable", menuName = "Scriptable Objects/FunctionalityAffectationTable")]
public class FunctionalityAffectationTable : ScriptableObject
{
    //[SerializeField] public Dictionary<Identity, List<SerializableType<Functionality>>> functionalityAffectationTable;

    public int nbRules;
    public List<KeyWordInRule> keyWords;
}

[Serializable]
public struct KeyWordInRule
{
    public GameObject keyWordPrefab;
    public Vector2Int posInRule;
}
