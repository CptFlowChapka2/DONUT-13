using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public FMODUnity.StudioEventEmitter deltaTest;

    private void Awake()
    {
        instance = this;
    }
}
