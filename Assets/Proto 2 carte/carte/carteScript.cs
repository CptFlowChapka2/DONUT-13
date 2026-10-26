using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class carteScript : MonoBehaviour
{
    [SerializeField] private Image cover;
    [SerializeField] private Image color;
    [SerializeField] private TextMeshProUGUI chiffre;

    public (bool played,bool drawn, int number, Color family) state;


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
    }
}
