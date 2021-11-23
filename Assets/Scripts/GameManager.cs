using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to start game, manage turns, 
public class GameManager : MonoBehaviour
{
    // Game Start Delegate + Event
    public delegate void GameStartEventHandler();
    public event GameStartEventHandler GameStarted;

    // Turn Start/End Delegate + Event

    public delegate void TurnStartEventHandler();
    public event TurnStartEventHandler TurnStarted;

    public delegate void TurnEndEventHandler();
    public event TurnEndEventHandler TurnEnded;



    public void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
        OnGameStarted();
    }

    public virtual void OnGameStarted()
    {
        GameStarted?.Invoke();
    }

    public virtual void OnTurnStarted()
    {
        TurnStarted?.Invoke();
    }

    public virtual void OnTurnEnded()
    {
        TurnEnded?.Invoke();
    }    
}
