using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class DeckScript : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform hand;

    private bool[,] possibleCards=new bool[4,12]
    {
        {true,true,true,true,true,true,true,true,true,true,true,true},
        {true,true,true,true,true,true,true,true,true,true,true,true},
        {true,true,true,true,true,true,true,true,true,true,true,true},
        {true,true,true,true,true,true,true,true,true,true,true,true}
    };

    

    public void Draw()
    {
        //genére une collection sans dupliqué d'index aléatoire
        HashSet<int> rSet=new HashSet<int>();
        while (rSet.Count!=possibleCards.Length)
        {
            rSet.Add(Random.Range(0, possibleCards.Length));
        }
        int[] rArray = rSet.ToArray();
        
        //setup pour la recherche
        bool passed = false;
        //initialise valeur de réponse
        (bool played, bool drawn, int number, Color family) answer = new ValueTuple<bool, bool, int, Color>();
        
        //fouile le deck
        foreach (var i in rArray)
        {
            if (!possibleCards[Mathf.FloorToInt(i / 12), i % 12]) continue;
            passed = true;
            possibleCards[Mathf.FloorToInt(i / 12), i % 12] = false;
            answer.drawn = true;
            answer.family = Mathf.FloorToInt(i / 12) switch
            {
                0 => Color.black,
                1 => Color.darkRed,
                2 => Color.darkBlue,
                3 => Color.darkGreen,
                _ => answer.family
            };
            answer.played = false;
            answer.number = i % 12 +1;
            break;
        }
        if (!passed)
        {
            //todo cycle deck
            Debug.LogWarning("empty deck");
        }

        //crée la carte
        var carteScript=Instantiate(cardPrefab, hand).GetComponent<carteScript>();
        carteScript.Create(answer);
    }
}
