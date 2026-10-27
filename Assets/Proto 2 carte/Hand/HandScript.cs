using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    private List<carteScript> cardInHand = new List<carteScript>();
    [SerializeField] private int distancePerCard = 100;

    public void AddCarte(carteScript cS)
    {
        cardInHand.Add(cS);
        cS.transform.SetParent(transform);
        cS.state.drawn = true;
        cS.c_Hand = this;
        PositionHandInLocalSpace();
        
    }

    public void RemoveCard(carteScript cS)
    {
        cardInHand.Remove(cS);
        PositionHandInLocalSpace();
        cS.c_Hand = null;
    }

    public void PositionHandInLocalSpace()
    {
        cardInHand.RemoveAll(x => x == null);
        for (int i = 0; i < cardInHand.Count; i++)
        {
            cardInHand[i].transform.localPosition = new Vector3(Mathf.Sign(-1*(i%2))*(i * distancePerCard), 0, 0);
        }
    }
}
