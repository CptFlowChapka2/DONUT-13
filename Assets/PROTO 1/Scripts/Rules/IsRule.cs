using UnityEngine;

public class IsRule : Rule
{
    public override void Evaluate()
    { 
        if(slots[0].slottedKeyWord.type==KeyWordType.None || slots[1].slottedKeyWord.type==KeyWordType.None) return;
        switch (slots[0].slottedKeyWord.type,slots[1].slottedKeyWord.type)
        {
          case (KeyWordType.Object,KeyWordType.Object) :
              slots[0].slottedKeyWord.referencedObject = slots[1].slottedKeyWord.referencedObject;
              slots[0].slottedKeyWord.referencedObject.gameObject.GetComponent<DefaultPreFabValues>().actualKeyword =
                  slots[1].slottedKeyWord.referencedObject.gameObject.GetComponent<DefaultPreFabValues>().actualKeyword;
              break;
          case (KeyWordType.Control,KeyWordType.Object) :
              slots[1].slottedKeyWord.referencedObject.gameObject.AddComponent<Player>();
              break;
          case (KeyWordType.Object,KeyWordType.Parameter) :
              slots[0].slottedKeyWord.referencedObject.gameObject.GetComponent<DefaultPreFabValues>()
                  .defaultVerbsParamsOverride
                  .TryAdd(slots[1].slottedKeyWord.paramValuePair.Item1,slots[1].slottedKeyWord.paramValuePair.Item2);
              break;
            
        }
        
    }
}
