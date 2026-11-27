using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    #region Singleton

    public static StateMachine instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion
    
    [SerializeField] private Dictionary<CumulState, PlayerState> allStates = new Dictionary<CumulState, PlayerState>();
    [SerializeField] private Player player;
    private CumulState currentCumulState = new CumulState();

    private void Update()
    {
        //Check un potentiel changement de cumulState pour ajuster le PlayerState
        if (!player.cumulState.Equals(currentCumulState))
        {
            if (player.playerState != allStates[player.cumulState])
            {
                ChangePlayerStateTo(allStates[player.cumulState]);
            }
            currentCumulState = player.cumulState;
        }  
    }

    public void ChangePlayerStateTo(PlayerState state)
    {
        player.playerState.OnExit();
        player.playerState = state;
        player.playerState.OnEnter();
    }
}
