using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllInventoryKeyWords", menuName = "Scriptable Objects/AllInventoryKeyWords")]
public class AllInventoryKeyWords : ScriptableObject
{
    public List<GameObject> inventoryKeyWordsPrefabs;
}
