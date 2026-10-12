using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public KeyWordType[] authorizedType;
    public KeyWordType[] curatedAuthorizedTypes;
    public KeyWord slottedKeyWord;
    public RectTransform slottedKeyWordTransform;

    public Vector2 anchorPoint;
    
    private InteractionManager interactionManager;
    private Vector2 velocity = new Vector2(0, 2f);
    private Vector2 parentPosition;

    private void Start()
    {
        interactionManager = FindAnyObjectByType<InteractionManager>();
        RectTransform parentRectTransform = transform.parent.GetComponent<RectTransform>();
        if (parentRectTransform.gameObject.CompareTag("Rule"))
        {
            parentPosition = Vector2.zero;
        }
        else
        {
            parentPosition = parentRectTransform.anchoredPosition;
        }
    }

    private void Update()
    {
        PlaceKeyWordCorrectly();
    }

    private void PlaceKeyWordCorrectly()
    {
        if (interactionManager.currentSelectedSlot == this || !slottedKeyWordTransform) return;
        slottedKeyWordTransform.anchoredPosition = Vector2.SmoothDamp(slottedKeyWordTransform.anchoredPosition, new Vector2(anchorPoint.x, anchorPoint.y) + parentPosition, ref velocity, 0.5f);
    }

    public void WhenMouseEnter()
    {
        Debug.Log("Hi " + name);
        interactionManager.highlightedSlot = this;
    }

    public void WhenMouseExit()
    {
        Debug.Log("Bye " + name);
        interactionManager.highlightedSlot = null;
    }
}
