using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionManager : MonoBehaviour
{
    public UnityEvent<KeyWord, Slot> onButtonPressed;
    
    public List<KeyWord> allUnlockedKeyWords;

    private KeyWord currentSelectedKeyWord;
    private Slot currentSelectedSlot;

    public void WhenButtonPressed(KeyWord word, Slot slot)
    {
        if (!slot)
        {
            Debug.Log("No Slot Selected");
            return;
        }
        
        //Sélection du premier mot
        if (!currentSelectedSlot)
        {
            currentSelectedKeyWord = word;
            currentSelectedSlot = slot;
            Debug.Log(currentSelectedKeyWord);
            //Effets visuels et sonores de sélection
            return;
        }
        
        Swap(word, currentSelectedKeyWord, slot, currentSelectedSlot);
        currentSelectedKeyWord = null;
        currentSelectedSlot = null;
    }

    public void Swap(KeyWord word1, KeyWord word2, Slot slot1, Slot slot2)
    {
        slot1.slottedKeyWord = word2;
        slot2.slottedKeyWord = word1;
        word1.GetComponent<RectTransform>().anchoredPosition = slot2.anchorPoint.GetComponent<RectTransform>().anchoredPosition;
        word2.GetComponent<RectTransform>().anchoredPosition = slot1.anchorPoint.GetComponent<RectTransform>().anchoredPosition;
    }
}
