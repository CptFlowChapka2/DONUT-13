using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMoover : Interactable
{
    public NavMeshAgent navAgent;
    private NavMeshPath _path;
    
    
    private void Awake()
    {
        //on initialise les valeur meta de cette classe
        thisMeta = CreateMeta(this);
        thisState = InternalState.Create();
    }
    
    [InteractableAction("MooveAction")]
    public InteractionValues Collect(InteractionValues inputs)
    {
        //on performe l'action
        var returnTuple= MooveAction(Cast<Vector3>(inputs.values[0]));
        //on wrap les info dans Interaction Values 
        InteractionValues toReturn = InteractionValues.Create(
            (0,returnTuple.Item1),
            (1,returnTuple.Item2));
        //on retourne
        return toReturn;
    }

    [InteractableEffect(new InteractionEffect[] 
        { InteractionEffect.ChangeAgentPosition})]
    public (InteractionError, KeyValuePair<string, (Type, object)>[]) MooveAction(Vector3 target)
    {
        var rL = new List<KeyValuePair<string, (Type,object)>> { };
        if (!navAgent.CalculatePath(target, _path))
        {
            return (InteractionError.NoPathToTarget, rL.ToArray());
        }

        navAgent.transform.position = target;
        return (InteractionError.Success, rL.ToArray());

    }
    
    
    
    
    public override InteractionValues TryActivate(string n, InteractionValues r)
    {
        return thisMeta.allInteractionsAvailable[n].action(r);
    }
}
