
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class InteractableVolume : MonoBehaviour
{
    public List<Interactable> allInteractables = new List<Interactable>();
    [SerializeField] public Collider collider;

    private void Start()
    {
        God.Instance?.allInteractableVolumes.Add(this);
    }
    
    public InteractionMonad[] AskForAction(InteractionEffect e)
    {
        HashSet<InteractionMonad> r = new HashSet<InteractionMonad>();
        foreach (var interactable in allInteractables)
        {
            r.AddRange(interactable.thisMeta.allInteractionsAvailable.Values.ToList().FindAll(x => x.Effects.Contains(e)));
        }

        return r.ToArray();
    }
}
