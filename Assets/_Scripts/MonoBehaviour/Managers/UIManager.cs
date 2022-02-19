using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : StaticInstance<UIManager>
{
    public Canvas GameOverCanvas;
    public Canvas PuaseCanvas;
    public Canvas DialougeCanvas;
    public Canvas SettingPauseCanvas;
    public Canvas StartCanvas;
    public Canvas WinCanvas;
    [SerializeField] Slider Enemeyhealth;

    [SerializeField] Slider FXVolumnStartCanv;

    [SerializeField] Slider MainVolumnStartCanv;
    [SerializeField] Toggle muteStartCanv;
    [SerializeField] Enemy enemy;
    [SerializeField] AudioSetting audioSetting;
    private void Awake()
    {
        MainVolumnStartCanv.value = audioSetting.MainAudioVolume;
        FXVolumnStartCanv.value = audioSetting.FXAudioVolume;
      //  muteStartCanv.Equals(audioSetting._mute);
    }
    private void Update()
    {
        audioSetting.FXAudioVolume = FXVolumnStartCanv.value;
        audioSetting.MainAudioVolume = MainVolumnStartCanv.value;
     //   audioSetting._mute = muteStartCanv;
        if (Input.GetKey(KeyCode.Escape))
        {
            GameManager.Instance.ChangeState(GameState.Puase);
            PuaseCanvas.gameObject.SetActive(true);
        }
        Enemeyhealth.value = enemy.currentHealth;
    }
    public void Restart()
    {
        GameOverCanvas.gameObject.SetActive(false);
        PuaseCanvas.gameObject.SetActive(false);
        SettingPauseCanvas.gameObject.SetActive(false);
        StartCanvas.gameObject.SetActive(false);
        PuaseCanvas.gameObject.SetActive(false);
        GameManager.Instance.Restart();
    }
    public void Resume()
    {
        GameOverCanvas.gameObject.SetActive(false);
        PuaseCanvas.gameObject.SetActive(false);
        SettingPauseCanvas.gameObject.SetActive(false);
        StartCanvas.gameObject.SetActive(false);
        PuaseCanvas.gameObject.SetActive(false);
        GameManager.Instance.ChangeState(GameState.Playing);
    }

}
