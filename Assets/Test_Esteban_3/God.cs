using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class God : MonoBehaviour
{
    private static God instance = null;
    public static God Instance => instance;
    public List<InteractableVolume> allInteractableVolumes = new List<InteractableVolume>();
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

        
        
    }

    private void Start()
    {
        List<Interactable> allInteractables = new List<Interactable>();
        allInteractables.AddRange(FindObjectsByType<Interactable>());
        foreach (var interactable in allInteractables)
        {
            allInteractableVolumes
                .FindAll(x=>x.collider.bounds
                    .Intersects(interactable.ownCollider.bounds))
                .ForEach(y=>y.allInteractables
                    .Add(interactable));
        }
    }
}
