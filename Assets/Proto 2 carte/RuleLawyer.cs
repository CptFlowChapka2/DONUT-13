using UnityEngine;

public class RuleLawyer : MonoBehaviour
{
    private static RuleLawyer instance = null;
    public static RuleLawyer Instance => instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }


    private carteScript waitingCard;
    public void DoSomethingWithCard(carteScript cS)
    {
        if (!cS.state.played)
        {
            Debug.Log("CardSelected");
            waitingCard = cS;
            return;
        }
        else
        {
            Debug.Log("CardPlayed");
            waitingCard.c_Hand.RemoveCard(waitingCard);
            cS.c_pile.AddCarteOnPile(waitingCard);
            waitingCard = null;
        }
    }
}
