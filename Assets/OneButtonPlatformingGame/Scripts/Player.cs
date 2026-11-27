using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CumulState cumulState;
    public PlayerState playerState;

    public bool rightOriented = true;

    [NonSerialized] public float t;

    private void Start()
    {
        cumulState = new CumulState(true, false, ActionState.None);
    }
    
    private void Update()
    {
        //Reset l'action du player au bout du temps donné pour cette action
        if (cumulState.currentAction == ActionState.None) return;
        t -= Time.deltaTime;
        if (t <= 0f)
        {
            cumulState.currentAction = ActionState.None;
        }
    }
}

[Serializable]
public struct CumulState
{
    public ActionState currentAction;
    public bool isGrounded;
    public bool isOnWall;

    public CumulState(bool a, bool b, ActionState c)
    {
        isGrounded = a;
        isOnWall = b;
        currentAction = c;
    }
    public bool Equals(CumulState other)
    {
        if (isGrounded == other.isGrounded && isOnWall == other.isOnWall && currentAction == other.currentAction)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public enum ActionState
{
    None,
    Jump,
    Dive,
    Slide,
    GroundPound,
    WallJump
}