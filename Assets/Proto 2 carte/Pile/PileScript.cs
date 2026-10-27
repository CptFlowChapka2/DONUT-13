using System;
using System.Collections.Generic;
using UnityEngine;

public class PileScript : MonoBehaviour
{
    private Stack<carteScript> pile = new Stack<carteScript>();
    [SerializeField] private carteScript startingCarte;

    private void Start()
    {
        startingCarte.ActivateButton();
        AddCarteOnPile(startingCarte);
    }


    public void AddCarteOnPile(carteScript cS)
    {
        if(cS is null) return;
        pile.Push(cS);
        cS.transform.SetParent(transform);
        cS.c_pile = this;
        cS.state.played = true;
        cS.transform.localPosition = Vector3.zero;
    }
}
