using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class Carte_Vector : MonoBehaviour
{
   public Image image;
   public GameObject arrow;
   private Vector2Int value;
   public TextMeshProUGUI texte;
   
   
   public Vector2Int Value
   {
      get => value;
      set
      {
         this.value = value;
         TurnArrow(value);
         texte.text = value.magnitude.ToString();
      }
   }

   public  UnityEvent<Carte_Vector> buttonPressed;

   public void butonPressed()
   {
      buttonPressed.Invoke(this);
   }

   private void TurnArrow(Vector2Int value)
   {
      arrow.gameObject.SetActive(true);
      if(value==Vector2Int.zero)arrow.gameObject.SetActive(false);
      float angle = Vector2.SignedAngle(Vector2Int.up, value);
      arrow.transform.rotation=Quaternion.AngleAxis(angle,arrow.transform.forward);
   }
}
