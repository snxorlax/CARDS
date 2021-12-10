using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to start game, manage turns, 
public class GameManager : MonoBehaviour
{
    // Game Start Delegate + Event
    public delegate void GameStartEventHandler();
    public static event GameStartEventHandler GameStarted;

    // Turn Start/End Delegate + Event

    public delegate void TurnStart();
    public static event TurnStart OnTurnStarted;

    public delegate void TurnEnd();
    public static event TurnEnd OnTurnEnded;



    //Starts the game when game loads
    public void Start()
    {
        StartGame();
    }
    //Starts the game
    public void StartGame()
    {
        OnGameStarted();
    }

    //Start a turn when the game starts
    public virtual void OnGameStarted()
    {
        GameStarted?.Invoke();
        Invoke("StartTurn", 2f);
    }

    //Trigger the first phase of a turn
    public virtual void StartTurn()
    {
        OnTurnStarted?.Invoke();
    }
    //Trigger the end of turn
    public virtual void EndTurn()
    {
        OnTurnEnded?.Invoke();
    }    


}
