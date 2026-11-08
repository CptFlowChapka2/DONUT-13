using System;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class ObjectiveInteractable : Interactable
{

    private void Awake()
    {
        //on initialise les valeur meta de cette classe
        thisMeta = CreateMeta(this);
        thisState = InternalState.Create();
    }
    
    [InteractableAction("CollectAction")]
    public InteractionValues Collect(InteractionValues inputs)
    {
        //on performe l'action
        var returnTuple= CollectAction(Cast<Agent>(inputs.values[0]));
        //on wrap les info dans Interaction Values 
        InteractionValues toReturn = InteractionValues.Create(
            (0,returnTuple.Item1),
            (1,returnTuple.Item2));
        //on retourne
        return toReturn;
    }

    [InteractableEffect(new[] 
        { InteractionEffect.modifySpace,InteractionEffect.GiveAgentTarget,
            InteractionEffect.ModifyAvailableInteractable,InteractionEffect.ChangeAgentState })]
    public (InteractionError, KeyValuePair<string, (Type,object)>[]) CollectAction(Agent agent)
    {
        var rL = new List<KeyValuePair<string, (Type,object)>> { };
        if (!ownCollider.bounds.Intersects(agent.collider.bounds))
        {
            KeyValuePair<string, (Type, object)> t =
                new KeyValuePair<string, (Type, object)>("target", (typeof(Vector3), transform.position));
            rL.Add(t);
            return (InteractionError.AgentAintCloseEnought, rL.ToArray());
        }
        Destroy(gameObject);
        God.Instance.allInteractableVolumes.Find(x => x.allInteractables.Contains(this)).allInteractables.Remove(this);
        rL.Add( new KeyValuePair<string, (Type,object)>("objective",(typeof(bool),true)));
        return (InteractionError.Success, rL.ToArray());
    }
    
    public override InteractionValues TryActivate(string n, InteractionValues r)
    {
        return thisMeta.allInteractionsAvailable[n].action(r);
    }
}
