using System;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public KeyWordType[] authorizedType;
    public KeyWord slottedKeyWord;
    
    private InteractionManager interactionManager;

    private void Start()
    {
        interactionManager = FindAnyObjectByType<InteractionManager>();
    }

    public void OnClick()
    {
        interactionManager.onButtonPressed.Invoke(slottedKeyWord, this);
    }
}
