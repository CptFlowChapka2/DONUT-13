using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    //public UnityEvent<KeyWord, Slot> onButtonPressed;
    
    public List<KeyWord> allUnlockedKeyWords;

    public KeyWord currentSelectedKeyWord;
    public Slot currentSelectedSlot;
    
    public Slot highlightedSlot;
    
    private RectTransform keywordRectTransform;

    private void Update()
    {
        if (!currentSelectedSlot) return;
        currentSelectedSlot.slottedKeyWordTransform.anchoredPosition = Mouse.current.position.ReadValue() - new Vector2(1920f/2f, 1080f/2f);
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.button != 0) return;
        if (e.type == EventType.MouseDown)
        {
            if (highlightedSlot)
            {
                currentSelectedSlot = highlightedSlot;
                currentSelectedKeyWord = highlightedSlot.slottedKeyWord;
            }
        }

        if (e.type == EventType.MouseUp)
        {
            if (highlightedSlot &&currentSelectedKeyWord &&highlightedSlot.authorizedType.Contains(currentSelectedKeyWord.type))
            {
                Swap(currentSelectedKeyWord, highlightedSlot.slottedKeyWord, currentSelectedSlot, highlightedSlot);
            }
            currentSelectedKeyWord = null;
            currentSelectedSlot = null;
            Debug.Log("Up");
        }
    }

    // public void WhenButtonPressed(KeyWord word, Slot slot)
    // {
    //     if (!slot)
    //     {
    //         Debug.Log("No Slot Selected");
    //         return;
    //     }
    //     
    //     //Sélection du premier mot
    //     if (!currentSelectedSlot)
    //     {
    //         currentSelectedKeyWord = word;
    //         currentSelectedSlot = slot;
    //         Debug.Log(currentSelectedKeyWord);
    //         //Effets visuels et sonores de sélection
    //         return;
    //     }
    //
    //     if (!slot.authorizedType.Contains(currentSelectedKeyWord.type))
    //     {
    //         Debug.Log("This KeyWordIsInvalid in regard to restriction");
    //         currentSelectedKeyWord = null;
    //         currentSelectedSlot = null;
    //         //todo feedback qui dis "non"
    //         return;
    //     }
    //     
    //     Swap(word, currentSelectedKeyWord, slot, currentSelectedSlot);
    //     currentSelectedKeyWord = null;
    //     currentSelectedSlot = null;
    // }

    public void Swap(KeyWord word1, KeyWord word2, Slot slot1, Slot slot2) 
    {
        slot1.slottedKeyWord = word2;
        slot1.slottedKeyWordTransform = word2.GetComponent<RectTransform>();
        slot2.slottedKeyWord = word1;
        slot2.slottedKeyWordTransform = word1.GetComponent<RectTransform>();
    }
}
