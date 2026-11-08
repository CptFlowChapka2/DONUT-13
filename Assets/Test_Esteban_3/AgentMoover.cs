using System;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
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
    public InteractionValues Moove(InteractionValues inputs)
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
        _path = new NavMeshPath();
        var rL = new List<KeyValuePair<string, (Type,object)>> { };
        NavMesh.SamplePosition(target, out NavMeshHit hit, ownCollider.bounds.extents.magnitude,NavMesh.AllAreas);
        target = hit.position;
        if (!hit.hit||!navAgent.CalculatePath(target, _path))
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
