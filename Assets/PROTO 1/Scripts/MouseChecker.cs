using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseChecker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Slot slot;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        slot.WhenMouseEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        slot.WhenMouseExit();
    }
}
