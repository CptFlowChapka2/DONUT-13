using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CanRule : Rule
{
    public override void Evaluate()
    {
        if(slots[0].slottedKeyWord.type==KeyWordType.None || slots[1].slottedKeyWord.type==KeyWordType.None) return;
        switch (slots[0].slottedKeyWord.type,slots[1].slottedKeyWord.type)
        {
            case (KeyWordType.Object,KeyWordType.Verb) :
                slots[1].slottedKeyWord.referencedObject.gameObject.AddComponent(slots[0].slottedKeyWord.test.GetType());
                break;
            
        }
    }
    
    [SerializeField]public Dictionary<SerializableType<Fonctionality>,Dictionary<identity,bool>> test2;

    void testo()
    {
        foreach (var keyValuePair in test2)
        {
            foreach (var test2Value in keyValuePair.Value)
            {
                //CRIME
                test2Value.Key.bob.ForEach(x=>x.SetComponentEnable(keyValuePair.Key.GetType(),test2Value.Value));
            }
        }
    }

}

public abstract class Fonctionality : MonoBehaviour
{
    public abstract void OnActivate();
    public abstract void OnDeActivate();
    public abstract void OnUpdate();

}
[Serializable]
public  class identity : MonoBehaviour
{
    const string nameE ="salut";
    public List<entityCentral> bob = new List<entityCentral>();
    
}

public class entityCentral : MonoBehaviour
{
    private Dictionary<Type, Fonctionality> boby;

    public void SetComponentEnable(Type t, bool b)
    {
        bool previouslyenabled = boby[t].enabled;
        if (!b && previouslyenabled) boby[t].OnDeActivate() ;
        boby[t].enabled = b;
        if (b && !previouslyenabled) boby[t].OnActivate() ;
    }

    private void Update()
    {
        foreach (var comp in boby.Values)
        {
            comp.OnUpdate();
        }
    }
}

