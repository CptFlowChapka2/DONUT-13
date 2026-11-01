using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class Carte_Vector : MonoBehaviour
{
   public Image image;
   public TextMeshProUGUI text;
   private Vector2Int value;

   public Vector2Int Value
   {
      get => value;
      set
      {
         this.value = value;
         text.text = value.ToString();
      }
   }

   public  UnityEvent<Carte_Vector> buttonPressed;

   public void butonPressed()
   {
      buttonPressed.Invoke(this);
   }
}
