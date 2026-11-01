using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaMain : MonoBehaviour
{
    public GameObject prefab;
    public List<Carte_Vector> carteInHands = new List<Carte_Vector> { };
    public UnityEvent<Carte_Vector> buttonPressed=new UnityEvent<Carte_Vector>();
    public GridManager gm;
    public float distancePerCard;
    public Carte_Vector lastCartePlayed;
    public Transform lastCard;

    private void Start()
    {
        buttonPressed.AddListener(CartePlayed);
        carteInHands.Add(CreateCard(new Vector2Int(0,-1)));
        carteInHands.Add(CreateCard(new Vector2Int(0,1)));
        carteInHands.Add(CreateCard(new Vector2Int(1,0)));
        carteInHands.Add(CreateCard(new Vector2Int(-1,0)));
        
        PositionHandInLocalSpace();
        
    }

    public void CartePlayed(Carte_Vector input)
    {
        if (lastCartePlayed == null)
        {
            lastCartePlayed = input;
            input.transform.localPosition = lastCard.localPosition;
            gm.MovePlayer(input.Value);
            carteInHands.Remove(input);
        }
        else
        {
            Vector2Int value = input.Value + lastCartePlayed.Value;
            if(value.magnitude>2) return;
            gm.MovePlayer(input.Value);
            if (lastCartePlayed.Value == Vector2Int.zero )
            {
                lastCartePlayed.Value = input.Value;
                carteInHands.Remove(lastCartePlayed);
                carteInHands.Remove(input);
                carteInHands.Add(lastCartePlayed);
                carteInHands.Add(input);
                lastCartePlayed = null;

            }
            else if ( input.Value == Vector2Int.zero)
            {
                input.Value = lastCartePlayed.Value;
                carteInHands.Remove(lastCartePlayed);
                carteInHands.Remove(input);
                carteInHands.Add(lastCartePlayed);
                carteInHands.Add(input);
                lastCartePlayed = null;
            }
            else
            {
                carteInHands.Remove(lastCartePlayed);
                input.Value = value;
                Destroy(lastCartePlayed.gameObject);
            }
            
            
        }
        PositionHandInLocalSpace();
    }
    

    public Carte_Vector CreateCard(Vector2Int value)
    {
        Carte_Vector toReturn;
        toReturn=Instantiate(prefab,transform).GetComponent<Carte_Vector>();
        toReturn.buttonPressed = buttonPressed;
        toReturn.Value = value;
        return toReturn;
    }
    
    public void PositionHandInLocalSpace()
    {
        carteInHands.RemoveAll(x => x == null);
        for (int i = 0; i < carteInHands.Count; i++)
        {
            carteInHands[i].transform.localPosition = new Vector3(Mathf.Sign(-1*(i%2))*(i * distancePerCard), 0, 0);
        }
    }
}
