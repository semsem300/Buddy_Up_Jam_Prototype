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
    Dialogue = 7
}
public class GameManager : StaticInstance<GameManager>
{
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    [SerializeField] Player player;
    [SerializeField] Enemy enemy;
    [SerializeField] AudioSetting setting;
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
            case GameState.Dialogue:
                HandleDialogue();
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
        AudioManager.Instance.StopSoundFxSource();
        AudioManager.Instance.StopSoundMainSource();
        AudioManager.Instance.PlaySoundMainSource(setting.mainThemeClip);
        // Eventually call ChangeState again with your next state
        SpawnPlayer();
        SpawnEnemy();
        //ChangeState(GameState.Playing);
    }

    private void SpawnEnemy()
    {
        enemy.enemyObj.transform.position = enemy.position;
    }

    private void SpawnPlayer()
    {
        player.playerObj.transform.position = player.position;
        //Instantiate(player.playerObj, player.position, Quaternion.identity);
    }

    private void HandlePlaying()
    {
        AudioManager.Instance.StopSoundMainSource();
        AudioManager.Instance.PlaySoundMainSource(setting.mainThemeClip);
    }
    private void HandleLose()
    {
        AudioManager.Instance.StopSoundFxSource();
        AudioManager.Instance.StopSoundMainSource();
        AudioManager.Instance.PlaySoundMainSource(setting.defeatThemeClip);
        UIManager.Instance.GameOverCanvas.gameObject.SetActive(true);
    }

    private void HandleWin()
    {
        AudioManager.Instance.StopSoundFxSource();
        AudioManager.Instance.StopSoundMainSource();
        AudioManager.Instance.PlaySoundMainSource(setting.winThemeClip);
        UIManager.Instance.WinCanvas.gameObject.SetActive(true);
    }
    private void HandleDialogue()
    {
        AudioManager.Instance.StopSoundMainSource();
        AudioManager.Instance.PlaySoundMainSource(setting.dialogueThemeClip);
    }
    private void HandlePuase()
    {
        // AudioManager.Instance.PlaySoundMainSource(setting.mainThemeClip);
    }
    public void Restart()
    {
        this.ChangeState(GameState.Playing);
        player.ResetPlayerHealth();
        enemy.ResetEnemy();
    }
}
