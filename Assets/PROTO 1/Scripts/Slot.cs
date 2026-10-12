using System;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public KeyWordType[] authorizedType;
    public KeyWordType[] curatedAuthorizedTypes;
    public KeyWord slottedKeyWord;

    public Vector2 anchorPoint;
    
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
