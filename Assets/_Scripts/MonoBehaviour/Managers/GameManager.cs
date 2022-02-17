using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[Serializable]
public enum GameState
{
    Starting = 0,
    SpawningHeroes = 1,
    SpawningEnemies = 2,
    Playing = 3,
    Puase = 4,
    Win = 5,
    Lose = 6,
}
public class GameManager : Singleton<GameManager>
{
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;
   
  
    public GameState State { get; private set; }

    // Kick the game off with the first state
    void Start() => ChangeState(GameState.Starting);

    public void ChangeState(GameState newState)
    {
        OnBeforeStateChanged?.Invoke(newState);
        State = newState;
        switch (newState)
        {
            case GameState.Starting:
                HandleStarting();
                break;
            case GameState.Playing:
                HandlePlaying();
                break;
            case GameState.Puase:
                HandlePuase();
                break;
            case GameState.Win:
                HandleWin();
                break;
            case GameState.Lose:
                HandleLose();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);

        Debug.Log($"New state: {newState}");
    }
    private void HandleStarting()
    {
        // Do some start setup, could be environment, cinematics etc

        // Loading 

        // Eventually call ChangeState again with your next state

        ChangeState(GameState.Playing);
    }
    private void HandlePlaying()
    {

    }
    private void HandleLose()
    {
        throw new NotImplementedException();
    }

    private void HandleWin()
    {
        throw new NotImplementedException();
    }

    private void HandlePuase()
    {
        throw new NotImplementedException();
    }

}
