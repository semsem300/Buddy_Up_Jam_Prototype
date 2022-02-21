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
[Serializable]
public class DuilogTime
{
    public GameObject Duilog;
    public float time;
}
public class GameManager : StaticInstance<GameManager>
{
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    [SerializeField] Player player;
    [SerializeField] Enemy enemy;
    [SerializeField] AudioSetting setting;
    [SerializeField] GameObject background;
    [SerializeField] DuilogTime[] StartDuilogs;
    [SerializeField] DuilogTime[] Stage2Duilogs;
    [SerializeField] DuilogTime[] Stage3Duilogs;
    [SerializeField] DuilogTime[] FinalDuilogs;
    [SerializeField] float startCanvasTime = 3f;
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

    public void ChangePattern(int currentPattern)
    {
        if (State == GameState.Playing)
        {
            switch (currentPattern)
            {
                case 0:
                    StartCoroutine(FirstScene());
                    break;
                case 1:
                    StartCoroutine(SecondScene());
                    break;
                case 2:
                    StartCoroutine(LastScene());
                    break;
            }
        }

    }

    private IEnumerator LastScene()
    {
        ChangeState(GameState.Dialogue);
        for (int i = 0; i < Stage3Duilogs.Length; i++)
        {
            GameObject Duilogs = Instantiate(Stage3Duilogs[i].Duilog);
            yield return new WaitForSeconds(Stage3Duilogs[i].time);
            Destroy(Duilogs);
        }
        yield return new WaitForSeconds(Stage3Duilogs[Stage3Duilogs.Length - 1].time);
        DialogueManager.Instance.EndDialogue();
        ChangeState(GameState.Playing);
        background.GetComponent<SpriteRenderer>().sprite = enemy.Background3;
        AudioManager.Instance.StopSoundMainSource();
        AudioManager.Instance.ChangeSoundMainSource(enemy.BossPhase3Clip);
    }

    private IEnumerator SecondScene()
    {
        ChangeState(GameState.Dialogue);
        for (int i = 0; i < Stage2Duilogs.Length; i++)
        {
            GameObject Duilogs = Instantiate(Stage2Duilogs[i].Duilog);
            yield return new WaitForSeconds(Stage2Duilogs[i].time);
            Destroy(Duilogs);
        }
        yield return new WaitForSeconds(Stage2Duilogs[Stage2Duilogs.Length - 1].time);
        DialogueManager.Instance.EndDialogue();
        ChangeState(GameState.Playing);
        AudioManager.Instance.StopSoundMainSource();

        background.GetComponent<SpriteRenderer>().sprite = enemy.Background2;
        AudioManager.Instance.ChangeSoundMainSource(enemy.BossPhase2Clip);
    }

    private IEnumerator FirstScene()
    {
        ChangeState(GameState.Dialogue);
        yield return new WaitForSeconds(startCanvasTime);
        UIManager.Instance.hidestartCanvas();
        for (int i = 0; i < StartDuilogs.Length; i++)
        {
            GameObject Duilogs = Instantiate(StartDuilogs[i].Duilog);
            yield return new WaitForSeconds(StartDuilogs[i].time);
            Destroy(Duilogs);

        }
        yield return new WaitForSeconds(StartDuilogs[StartDuilogs.Length - 1].time);
        DialogueManager.Instance.EndDialogue();
        //Destroy(Duilogs);
        ChangeState(GameState.Playing);
        background.GetComponent<SpriteRenderer>().sprite = enemy.Background1;
        AudioManager.Instance.ChangeSoundMainSource(enemy.BossPhase1Clip);
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
        StartCoroutine(WinCanvas());
       
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
       
        StartCoroutine(FirstScene());
        this.ChangeState(GameState.Playing);
        player.ResetPlayerHealth();
        enemy.ResetEnemy();
    }
   IEnumerator WinCanvas()
    {
        ChangeState(GameState.Dialogue);
        for (int i = 0; i < FinalDuilogs.Length; i++)
        {
            GameObject Duilogs = Instantiate(FinalDuilogs[i].Duilog);
            yield return new WaitForSeconds(FinalDuilogs[i].time);
            Destroy(Duilogs);
        }
        yield return new WaitForSeconds(FinalDuilogs[FinalDuilogs.Length - 1].time);
        ChangeState(GameState.Win);
        AudioManager.Instance.StopSoundFxSource();
        AudioManager.Instance.StopSoundMainSource();
        AudioManager.Instance.PlaySoundMainSource(setting.winThemeClip);
        UIManager.Instance.Epilogue_BeforeCredits.gameObject.SetActive(true);
    }
}

