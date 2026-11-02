
using System;
using System.Collections.Generic;
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
}
