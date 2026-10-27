using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class carteScript : MonoBehaviour
{
    [SerializeField] private Image cover;
    [SerializeField] private Image color;
    [SerializeField] private TextMeshProUGUI chiffre;
    [SerializeField] private Button button;
    private UnityEvent<carteScript> buttonPressed = new UnityEvent<carteScript>();
    [NonSerialized] public PileScript c_pile;
    [NonSerialized] public HandScript c_Hand;
    

    public (bool played,bool drawn, int number, Color family) state=new ValueTuple<bool, bool, int, Color>();


    public void Flip()
    {
        cover.gameObject.SetActive(!cover.gameObject.activeSelf);
        state.drawn = !state.drawn;
    }

    public void Create((bool played,bool drawn, int number, Color family) setup)
    {
        state = setup;
        chiffre.text = state.number.ToString();
        color.color = state.family;
        cover.gameObject.SetActive(!setup.drawn);
        ActivateButton();
    }

    public void ActivateButton()
    {
        button.onClick.AddListener(EmiteButton);
        buttonPressed.AddListener(RuleLawyer.Instance.DoSomethingWithCard);
    }

    private void EmiteButton()
    {
        buttonPressed.Invoke(this);
    }

    private void OnDestroy()
    {
        buttonPressed.RemoveAllListeners();
    }
}
