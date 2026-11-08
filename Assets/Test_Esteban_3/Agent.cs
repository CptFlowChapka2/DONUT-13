using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private AgentBrain brain = new AgentBrain();

    private bool testb = true;
    [SerializeField] public Collider collider;

    private void Awake()
    {
        brain.thisState.state.TryAdd("thisAgent", (typeof(Agent), this));
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.A)) return;
        AskForCurrentVolume();
        brain.AttainObjective();
        Debug.Log(brain.thisState.state.ContainsKey("objective"));
    }

    private void AskForCurrentVolume()
    {
        brain.currentVolume =
            God.Instance.allInteractableVolumes.FindAll(x => x.collider.bounds.Contains(transform.position));
    }
}


public class AgentBrain
{
    public InternalState thisState = InternalState.Create();

    public InternalState wantedState = InternalState.Create(("objective", (typeof(bool), true)));

    public List<InteractableVolume> currentVolume = new List<InteractableVolume>();


    public void AttainObjective()
    {
        var toDo = wantedState.state.Except(thisState.state);
        if (toDo.Any())
        {
            RecusivlyDoSomething(InteractionEffect.ChangeAgentState,0,out var attemptout);
        }
    }

    private InteractionError RecusivlyDoSomething(InteractionEffect effect,int attemptin,out int attemptout)
    {
        attemptout = attemptin + 1;
        if (attemptout >= 5) return InteractionError.NoActionLeadToSolution;
        var taskAtHand = CreateTaskAtHand(effect);
        InteractionError[] errors = new InteractionError[taskAtHand.actions.Length];
        for (int i = 0; i < taskAtHand.actions.Length; i++)
        {
            InteractionError code = TryToDoSomething(taskAtHand.actions[i]);
            ;
            if (code == InteractionError.Success)
            {
                return InteractionError.Success;
            }
            
            errors[i] = code;
        }

        for (int i = 0; i < errors.Length; i++)
        {
            InteractionEffect todo;
            switch (errors[i])
            {
                case InteractionError.AgentAintCloseEnought:
                    todo = InteractionEffect.ChangeAgentPosition;
                    break;
                case InteractionError.NoActionLeadToSolution:
                    continue;
                case InteractionError.NoPathToTarget:
                    todo = InteractionEffect.modifySpace;
                    break;
                case InteractionError.Success:
                default:
                    throw new ArgumentOutOfRangeException();
            }

            InteractionError code = RecusivlyDoSomething(todo,attemptout, out int attemptouot);
            attemptout += attemptouot;
            if (attemptout >= 5) return InteractionError.NoActionLeadToSolution;
            
            if (code == InteractionError.Success)
            {
                return RecusivlyDoSomething(effect, attemptin, out attemptout);
            }
        }

        return InteractionError.NoActionLeadToSolution;
    }


    public InteractionError TryToDoSomething(InteractionMonad monad)
    {
        var actionAttemped = monad.action.Invoke(MakeIV(monad));
        if (actionAttemped.values[0].GetType() == typeof(InteractionError) &&
            (InteractionError)actionAttemped.values[0] == InteractionError.Success)
        {
            PropagateSideEffect(monad, actionAttemped);
            return (InteractionError.Success);
        }
        else
        {
            PropagateSideEffect(monad, actionAttemped);
            return (InteractionError)actionAttemped.values[0];
        }
    }

    private InteractionTask CreateTaskAtHand(InteractionEffect effect)
    {
        HashSet<InteractionMonad> set = new HashSet<InteractionMonad>();
        foreach (var interactableVolume in currentVolume)
        {
            foreach (var interactableVolumeAllInteractable in interactableVolume.allInteractables)
            {
                foreach (var keyValuePair in interactableVolumeAllInteractable.thisMeta.allInteractionsAvailable)
                {
                    if (keyValuePair.Value.Effects.Contains(effect)) set.Add(keyValuePair.Value);
                }
            }
        }
        

        InteractionTask taskAtHand = new InteractionTask(effect, set.ToArray(), 0);
        return taskAtHand;
    }

    private InteractionValues MakeIV(InteractionMonad monad)
    {
        InteractionValues iV = InteractionValues.Create();
        foreach (var keyValuePair in monad.inputGuide)
        {
            iV.values.TryAdd(keyValuePair.Key, thisState.state.Values.First(x => x.Item1 == keyValuePair.Value).Item2);
        }

        return iV;
    }

    private void PropagateSideEffect(InteractionMonad monad, InteractionValues values)
    {
        bool check = false;
        foreach (var interactionEffect in monad.Effects)
        {
            if (interactionEffect == InteractionEffect.modifySpace)
            {
                check = true;
            }
            if (interactionEffect == InteractionEffect.ChangeAgentPosition)
            {
                check = true; 
            }
            if (interactionEffect == InteractionEffect.GiveAgentTarget)
            {
                check = true;

                int index = monad.outpoutGuide.Keys.ToList().Find(x =>
                    monad.outpoutGuide[x] == typeof(KeyValuePair<string, (Type, object)>[]));
                object o = values.values[index];
                var cValues = Cast<KeyValuePair<string, (Type, object)>[]>(o);
                foreach (var keyValuePair in cValues)
                {
                    thisState.state[keyValuePair.Key] = keyValuePair.Value;
                }
            }

            if (interactionEffect == InteractionEffect.ChangeAgentState)
            {
                check = true;

                int index = monad.outpoutGuide.Keys.ToList().Find(x =>
                    monad.outpoutGuide[x] == typeof(KeyValuePair<string, (Type, object)>[]));
                object o = values.values[index];
                var cValues = Cast<KeyValuePair<string, (Type, object)>[]>(o);
                foreach (var keyValuePair in cValues)
                {
                    thisState.state[keyValuePair.Key] = keyValuePair.Value;
                }
            }

            if (interactionEffect == InteractionEffect.ChangeInteractableState)
            {
                check = true;
            }

            if (interactionEffect == InteractionEffect.ModifyAvailableInteractable)
            {
                check = true;
            }
            
            else if (!check)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }

    private T Cast<T>(object o)
    {
        return (T)o;
    }

    // private InteractionEffect ChooseNextAction(InteractionError error)
    // {
    //     
    // }
}

public struct InteractionTask
{
    public InteractionEffect target;
    public InteractionMonad[] actions;
    public int actionIndexToGetBackTo;

    public InteractionTask(InteractionEffect t, InteractionMonad[] a, int i)
    {
        target = t;
        actions = a;
        actionIndexToGetBackTo = i;
    }
}