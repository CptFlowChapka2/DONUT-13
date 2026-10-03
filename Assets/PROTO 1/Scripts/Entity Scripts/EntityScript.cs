using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityScript : MonoBehaviour
{
    private EntityPrefabHolder holder;
    private KeyWord currentKeyword;
    [SerializeField] private GameObject replacable;
    private void Start()
    {
        holder = FindAnyObjectByType<EntityPrefabHolder>();
        holder.allEntities.Add(this);
    }

    public void ReplaceIdentity(GameObject newIdentity)
    {
        var nI= Instantiate(newIdentity, this.transform);
        List<Verb> allComponentInNew=new List<Verb>();
        List<Verb> allComponentInOld=new List<Verb>();
        nI.GetComponents<Verb>(allComponentInNew);
        replacable.GetComponents<Verb>(allComponentInOld);
        List<Verb> sameVerbsInNew =allComponentInNew.Intersect(allComponentInOld).ToList();
        List<Verb> sameVerbsInOld =allComponentInOld.Intersect(allComponentInNew).ToList();

        for (int i = 0; i < sameVerbsInNew.Count; i++)
        {
            sameVerbsInNew[i].ReplaceValue(sameVerbsInOld[i]);
        }
        
        Destroy(replacable);
        replacable = nI;
    }
    
}
