using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private AgentBrain brain = new AgentBrain();
    
    private bool testb=true;
    [SerializeField] public Collider collider;

    private void Awake()
    {
        brain.thisState.state.TryAdd("thisAgent", (typeof(Agent),this));
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.A)) return;
        
        AskForCurrentVolume();
        Debug.Log(brain.TryToDoSomething(InteractionEffect.ChangeAgentState));
        
        
    }

    private void AskForCurrentVolume()
    {
        brain.currentVolume =
            God.Instance.allInteractableVolumes.FindAll(x => x.collider.bounds.Contains(transform.position));
    }
}


public class AgentBrain
{
    public InternalState thisState=InternalState.Create();

    public InternalState wantedState = InternalState.Create(("objective", (typeof(bool),true)));
    
    public List<InteractableVolume> currentVolume=new List<InteractableVolume>();

    private Stack<InteractionTask> tasks = new Stack<InteractionTask>();

    
    
    
    public InteractionError TryToDoSomething(InteractionEffect effect)
    {
        var taskAtHand = CreateTaskAtHand(effect, out var set);
        var monad = taskAtHand.actions[taskAtHand.actionIndexToGetBackTo];
        
        var actionAttemped = monad.action.Invoke(MakeIV(monad));
        if (actionAttemped.values[0].GetType() == typeof(InteractionError) &&
            (InteractionError)actionAttemped.values[0] == InteractionError.Success)
        {
            PropagateSideEffect(monad,actionAttemped);
            return (InteractionError.Success );
        }
        else
        {
            return (InteractionError)actionAttemped.values[0];
        }
        


    }

    private InteractionTask CreateTaskAtHand(InteractionEffect effect, out HashSet<InteractionMonad> set)
    {
        set = new HashSet<InteractionMonad>();
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
        InteractionTask taskAtHand = new InteractionTask(effect,set.ToArray(),0);
        return taskAtHand;
    }

    private InteractionValues MakeIV(InteractionMonad monad)
    {
        InteractionValues iV = InteractionValues.Create() ;
        foreach (var keyValuePair in monad.inputGuide)
        {
            iV.values.TryAdd(keyValuePair.Key, thisState.state.Values.First(x => x.Item1 == keyValuePair.Value).Item2);
        }

        return iV;
    }

    private void PropagateSideEffect(InteractionMonad monad,InteractionValues values)
    {
        foreach (var interactionEffect in monad.Effects)
        {
            switch (interactionEffect)
            {
                case InteractionEffect.modifySpace:
                    break;
                case InteractionEffect.ChangeAgentState:
                    int index = monad.outpoutGuide.Keys.ToList().Find(x =>
                        monad.outpoutGuide[x] == typeof(KeyValuePair<string, (Type, object)>[]));
                    object o = values.values[index];
                    var cValues= Cast<KeyValuePair<string, (Type, object)>[]>(o);
                    foreach (var keyValuePair in cValues)
                    {
                        if (thisState.state.ContainsKey(keyValuePair.Key))
                        {
                            thisState.state[keyValuePair.Key] = keyValuePair.Value;
                        }
                        else
                        {
                            thisState.state.Add(keyValuePair.Key, keyValuePair.Value);
                        }
                    }
                    break;
                case InteractionEffect.ChangeInteractableState:
                    break;
                case InteractionEffect.ModifyAvailableInteractable:
                    break;
                default:
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

    public InteractionTask(InteractionEffect t,InteractionMonad[] a,int i)
    {
        target = t;
        actions = a;
        actionIndexToGetBackTo = i;
    }
    
}
