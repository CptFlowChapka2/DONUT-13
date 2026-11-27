using System;
using System.Collections.Generic;
using UnityEngine;

public class AlmightyButton : MonoBehaviour
{
    #region Singleton

    public static AlmightyButton instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    [SerializeField] private Player player;
    [SerializeField] private Dictionary<PlayerState, ActionState> actionsToPerformDependingOnPlayerState = new Dictionary<PlayerState, ActionState>();

    public void OnClick()
    {
        player.cumulState.currentAction =
            actionsToPerformDependingOnPlayerState[player.playerState];
    }
}
