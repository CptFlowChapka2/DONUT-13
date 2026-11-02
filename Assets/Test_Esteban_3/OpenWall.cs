using System;
using System.Collections.Generic;
using UnityEngine;

public class OpenWall : Interactable
{
    //obligatoire 
    private void Awake()
    {
        //on initialise les valeur meta de cette classe
        thisMeta = CreateMeta(this);
    }

    //on marque cet method comme action apelable .
    //et on spécifice quelle réelle fonction elle appelle
    [InteractableAction("OpenAction")]
    public InteractionValues Open(InteractionValues inputs)
    { 
        //on performe l'action
        var returnTuple= OpenAction(Cast<bool>(inputs.values[0]));
        //on wrap les info dans Interaction Values 
        InteractionValues toReturn = InteractionValues.Create();
        toReturn.values.TryAdd(0, returnTuple.Item1);
        toReturn.values.TryAdd(1, returnTuple.Item2);
        //on retourne
        return toReturn;
    }
    
    //une vrais action : par convention on retourne toujour un tuple qui as en premier object un bool indiquand le succé de l'opération ou non
    public (bool,bool) OpenAction(bool test)
    {
        transform.position = test switch
        {
            true => new Vector3(transform.position.x,10,transform.position.z),
            false => new Vector3(transform.position.x,0,transform.position.z)
        };
        return (true,!test);
    }

    //parreil partout ,c'ette methode interface fais qu'on peux appeler absolument n'importe qu'elle type d'action
    public override InteractionValues TryActivate(string n, InteractionValues r)
    {
        return thisMeta.allInteractionsAvailable[n].action(r);
    }
}