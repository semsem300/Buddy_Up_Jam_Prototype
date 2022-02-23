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
    [SerializeField] bool skipFirst = false;
    [SerializeField] bool skipSecond = false;
    [SerializeField] bool skipThird = false;
    [SerializeField] bool skipFinal = false;
    int stage = 0;
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
                    stage = 0;
                    StartCoroutine(FirstScene());
                    break;
                case 1:
                    stage = 1;
                    StartCoroutine(SecondScene());
                    break;
                case 2:
                    stage = 2;
                    StartCoroutine(LastScene());
                    break;
                case 3:
                    stage = 3;
                    StartCoroutine(DisplayFinalDialogue());
                    break;
            }
        }

    }


    public void SkipDialogue()
    {
        //DialogueManager.Instance.EndDialogue();
        switch (stage)
        {
            case 0:
                skipFirst = true;
                StopAllCoroutines();
                StartCoroutine(FirstScene());
                ChangeState(GameState.Playing);
                break;
            case 1:
                skipSecond = true;
                StopAllCoroutines();
                StartCoroutine(SecondScene());
                ChangeState(GameState.Playing);
                break;
            case 2:
                skipThird = true;
                StopAllCoroutines();
                StartCoroutine(LastScene());
                ChangeState(GameState.Playing);
                break;
            case 3:
                skipFinal = true;
                StopAllCoroutines();
                StartCoroutine(DisplayFinalDialogue());
                break;
        }

    }
    private void HandleStarting()
    {
        // Do some start setup, could be environment, cinematics etc

        // Loading 
        AudioManager.Instance.StopSoundFxSource();
        //AudioManager.Instance.StopSoundMainSource();
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
        //  AudioManager.Instance.PlaySoundMainSource(setting.bossPhase4_1Clip);
    }
    private void HandleLose()
    {
        AudioManager.Instance.StopSoundFxSource();
        // AudioManager.Instance.StopSoundMainSource();
        AudioManager.Instance.PlaySoundMainSource(setting.defeatThemeClip);
        UIManager.Instance.GameOverCanvas.gameObject.SetActive(true);
    }

    private void HandleWin()
    {
        AudioManager.Instance.StopSoundFxSource();
        AudioManager.Instance.PlaySoundMainSource(setting.winThemeClip);
        StartCoroutine(WinCanvas());
    }
    private void HandleDialogue()
    {
        AudioManager.Instance.PlaySoundMainSource(setting.dialogueThemeClip);
    }
    private void HandlePuase()
    {
        // AudioManager.Instance.PlaySoundMainSource(setting.mainThemeClip);
    }
    public void Restart()
    {
        skipFirst = false;
        skipSecond = false;
        skipThird = false;
        skipFinal = false;
        StartCoroutine(FirstScene());
        this.ChangeState(GameState.Playing);
        player.ResetPlayerHealth();
        enemy.ResetEnemy();
    }
    IEnumerator WinCanvas()
    {
        if (!skipFinal)
        {
            yield return new WaitForSeconds(FinalDuilogs[FinalDuilogs.Length - 1].time);
        }
        UIManager.Instance.Epilogue_BeforeCredits.gameObject.SetActive(true);
    }
    private IEnumerator FirstScene()
    {

        background.GetComponent<SpriteRenderer>().sprite = enemy.Background1;
        AudioManager.Instance.PlaySoundMainSource(enemy.BossPhase1Clip);
        ChangeState(GameState.Dialogue);
        if (!skipFirst)
        {
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
        }
        else
        {
            for (int i = 0; i < StartDuilogs.Length; i++)
            {
                StartDuilogs[i] = null;
            }
            DialogueManager.Instance.EndDialogue();

            yield return null;
        }
    }

    private IEnumerator SecondScene()
    {
        background.GetComponent<SpriteRenderer>().sprite = enemy.Background2;
        AudioManager.Instance.PlaySoundMainSource(enemy.BossPhase2Clip);
        ChangeState(GameState.Dialogue);
        if (!skipSecond)
        {
            for (int i = 0; i < Stage2Duilogs.Length; i++)
            {
                GameObject Duilogs = Instantiate(Stage2Duilogs[i].Duilog);
                yield return new WaitForSeconds(Stage2Duilogs[i].time);
                Destroy(Duilogs);
            }
            yield return new WaitForSeconds(Stage2Duilogs[Stage2Duilogs.Length - 1].time);
            DialogueManager.Instance.EndDialogue();
            ChangeState(GameState.Playing);
        }
        else
        {
            for (int i = 0; i < Stage2Duilogs.Length; i++)
            {
                Stage2Duilogs[i] = null;
            }
            DialogueManager.Instance.EndDialogue();

            yield return null;
        }
    }

    private IEnumerator LastScene()
    {
        background.GetComponent<SpriteRenderer>().sprite = enemy.Background3;
        AudioManager.Instance.PlaySoundMainSource(enemy.BossPhase3Clip);
        ChangeState(GameState.Dialogue);
        if (!skipThird)
        {
            for (int i = 0; i < Stage3Duilogs.Length; i++)
            {
                GameObject Duilogs = Instantiate(Stage3Duilogs[i].Duilog);
                yield return new WaitForSeconds(Stage3Duilogs[i].time);
                Destroy(Duilogs);
            }
            yield return new WaitForSeconds(Stage3Duilogs[Stage3Duilogs.Length - 1].time);
            DialogueManager.Instance.EndDialogue();
            ChangeState(GameState.Playing);
        }
        else
        {
            for (int i = 0; i < Stage3Duilogs.Length; i++)
            {
                Stage3Duilogs[i] = null;
            }
            DialogueManager.Instance.EndDialogue();

            yield return null;
        }
    }

    IEnumerator DisplayFinalDialogue()
    {
        ChangeState(GameState.Dialogue);
        if (!skipFinal)
        {
            for (int i = 0; i < FinalDuilogs.Length; i++)
            {
                GameObject Duilogs = Instantiate(FinalDuilogs[i].Duilog);
                yield return new WaitForSeconds(FinalDuilogs[i].time);
                Destroy(Duilogs);
            }
            yield return new WaitForSeconds(10);
            DialogueManager.Instance.EndDialogue();
        }
        else
        {
            for (int i = 0; i < FinalDuilogs.Length; i++)
            {
                FinalDuilogs[i] = null;
            }
            DialogueManager.Instance.EndDialogue();

            yield return null;
        }

    }
}
