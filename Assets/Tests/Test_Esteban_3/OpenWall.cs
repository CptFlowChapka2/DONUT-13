using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OpenWall : Interactable
{
    
    
    private void Awake()
    {
        //on initialise les valeur meta de cette classe
        thisMeta = CreateMeta(this);
        thisState = InternalState.Create(("open", (typeof(bool),false)));
    }
    
    //on marque cet method comme action apelable .
    //et on spécifice quelle réelle fonction elle appelle
    [InteractableAction("OpenAction")]
    public InteractionValues Open(InteractionValues inputs)
    { 
        //on performe l'action
        var returnTuple= OpenAction(Cast<Agent>(inputs.values[0]));
        //on wrap les info dans Interaction Values 
        InteractionValues toReturn = InteractionValues.Create(
            (0,returnTuple.Item1),
            (1,returnTuple.Item2));
        //on retourne
        return toReturn;
    }
    
    //une vrais action : par convention on retourne toujour un tuple qui as en premier object un bool indiquand le succé de l'opération ou non
    [InteractableEffect(new InteractionEffect[]
    {
        InteractionEffect.modifySpace , InteractionEffect.ChangeInteractableState 
        ,InteractionEffect.GiveAgentTarget
    })]
    public (InteractionError,KeyValuePair<string,(Type,object)>[]) OpenAction(Agent agent)
    {
        var rL = new List<KeyValuePair<string, (Type,object)>> { };
        if (!ownCollider.bounds.Intersects(agent.collider.bounds))
        {
            KeyValuePair<string, (Type, object)> t =
                new KeyValuePair<string, (Type, object)>("target", (typeof(Vector3), transform.position));
            rL.Add(t);
            return (InteractionError.AgentAintCloseEnought, rL.ToArray());
        }

        switch (thisState.state["open"].Item2)
        {
            case true:
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                thisState.state["open"] = (typeof(bool), false);
                break;
            case false:
                transform.position = new Vector3(transform.position.x, 15, transform.position.z);
                thisState.state["open"] = (typeof(bool), true);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        var d = thisState.state.First(x => x.Key == "open");
        rL.Add(d);
        return (InteractionError.Success,rL.ToArray());
    }

    //parreil partout ,c'ette methode interface fais qu'on peux appeler absolument n'importe qu'elle type d'action
    public override InteractionValues TryActivate(string n, InteractionValues r)
    {
        return thisMeta.allInteractionsAvailable[n].action(r);
    }
}