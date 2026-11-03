using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

//bienvenu en enfer
public abstract class Interactable : MonoBehaviour
{
    public Collider ownCollider;

    public InteractableMeta thisMeta;
    public InternalState thisState;


    public abstract InteractionValues TryActivate(string n, InteractionValues r);

    protected static InteractableMeta CreateMeta<T>(T o) where T : Interactable
    {
        InteractableMeta toReturn = InteractableMeta.Create();
        var methods = typeof(T).GetMethods();
        foreach (var methodInfo in methods)
        {
            if (methodInfo.GetCustomAttributes(true).All(x => x.GetType() != typeof(InteractableAction))) continue;

            var dictionary = new Dictionary<int, Type>();
            var Rdictionary = new Dictionary<int, Type>();
            var targetedMethod = methods.ToList().Find(x =>
                x.Name == ((InteractableAction)methodInfo.GetCustomAttributes(typeof(InteractableAction), true).First())
                .Name);
            var parameterInfos = targetedMethod.GetParameters();
            var returnInfos = targetedMethod.ReturnParameter?.ParameterType.GetFields();

            for (var i = 0; i < parameterInfos.Length; i++)
            {
                dictionary.TryAdd(i, parameterInfos[i].ParameterType);
            }

            for (var i = 0; i < returnInfos.Length; i++)
            {
                Rdictionary.TryAdd(i, returnInfos[i].FieldType);
            }

            InteractionMonad monad = InteractionMonad.Create(
                methodInfo.Name, dictionary, Rdictionary,
                (Func<InteractionValues, InteractionValues>)methodInfo.CreateDelegate(
                    typeof(Func<InteractionValues, InteractionValues>), o),
                ((InteractableEffect)targetedMethod.GetCustomAttributes(typeof(InteractableEffect), true).First())
                .Effects);
            toReturn.allInteractionsAvailable.TryAdd(methodInfo.Name, monad);
        }

        return toReturn;
    }

    protected T Cast<T>(object o)
    {
        return (T)o;
    }
}


//les struct et autre utilitaire du Interactable System


//Interatable meta : struct contenant tout les question qu'on peux se poser sur un interactable avec qui on interagit à l'aveugle
//intent : stocker et crée une fois sur un Interactable
[Serializable]
public struct InteractableMeta
{
    public Dictionary<string, InteractionMonad> allInteractionsAvailable;

    public static InteractableMeta Create()
    {
        InteractableMeta r = new InteractableMeta();
        r.allInteractionsAvailable = new Dictionary<string, InteractionMonad>();
        return r;
    }
}

//La "monade " (c'en est pas vraiment une stricto sensu) elle contient tout les information sur une methode InteractableAction
[Serializable]
public struct InteractionMonad
{
    public string name;
    public ReadOnlyDictionary<int, Type> inputGuide;
    public ReadOnlyDictionary<int, Type> outpoutGuide;
    public InteractionEffect[] Effects;
    public Func<InteractionValues, InteractionValues> action;


    public static InteractionMonad Create(string n, Dictionary<int, Type> a, Dictionary<int, Type> b,
        Func<InteractionValues, InteractionValues> c, InteractionEffect[] e)
    {
        InteractionMonad r = new InteractionMonad();
        r.inputGuide = new ReadOnlyDictionary<int, Type>(a);
        r.outpoutGuide = new ReadOnlyDictionary<int, Type>(b);
        r.action = c;
        r.Effects = e;
        r.name = n;
        return r;
    }
}

[Serializable]
public struct InteractionValues
{
    public Dictionary<int, object> values;

    public static InteractionValues Create(params (int, object)[] input)
    {
        InteractionValues r = new InteractionValues();
        r.values = new Dictionary<int, object>();
        foreach (var valueTuple in input)
        {
            r.values.TryAdd(valueTuple.Item1, valueTuple.Item2);
        }

        return r;
    }
}

public class InteractableAction : System.Attribute
{
    public string Name;

    public InteractableAction(string name)
    {
        Name = name;
    }
}

public class InteractableEffect : System.Attribute
{
    public InteractionEffect[] Effects;

    public InteractableEffect(InteractionEffect[] input)
    {
        Effects = input;
    }
}

public enum InteractionError
{
    Success,
    AgentAintCloseEnought
}

public enum InteractionEffect
{
    modifySpace,
    ChangeAgentState,
    ChangeInteractableState,
    ModifyAvailableInteractable
}

public struct InternalState
{
    public Dictionary<string, (Type, object)> state;

    public static InternalState Create(params (string, (Type, object))[] par)
    {
        InternalState r = new InternalState();
        r.state = new Dictionary<string, (Type, object)>();
        foreach (var valueTuple in par)
        {
            r.state.TryAdd(valueTuple.Item1, valueTuple.Item2);
        }

        return r;
    }
}