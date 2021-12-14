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

    //Draw Phase Delegate + Event
    public delegate void StartDrawPhase();
    public static event StartDrawPhase OnDrawStarted;

    //Main Phase Delegate + Event
    public delegate void StartMainPhase();
    public static event StartMainPhase OnMainStarted;

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
        Invoke("StartTurn", 1.5f);
    }

    //Trigger the first phase of a turn
    public virtual void StartTurn()
    {
        OnTurnStarted?.Invoke();
        Invoke("DrawPhase", 1.5f);
    }
    //Trigger draw phase
    public virtual void DrawPhase()
    {
        OnDrawStarted?.Invoke();
        Invoke("MainPhase", 1.5f);
    }
    //Trigger main phase
    public virtual void MainPhase()
    {
        OnMainStarted?.Invoke();
    }
    //Trigger the end of turn
    public virtual void EndTurn()
    {
        OnTurnEnded?.Invoke();

    }    

    public void AllCoroutines()
    {

    }


}
