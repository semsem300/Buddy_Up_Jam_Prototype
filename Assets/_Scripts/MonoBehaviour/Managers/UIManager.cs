using System;
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
    public Canvas Prologue_AfterMainMenu;
    public Canvas Epilogue_BeforeCredits;
    [SerializeField] Slider Enemeyhealth;

    [SerializeField] Slider FXVolumnStartCanv;

    [SerializeField] Slider MainVolumnStartCanv;
    [SerializeField] Toggle muteStartCanv;
    [SerializeField] Enemy enemy;
    [SerializeField] AudioSetting audioSetting;

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
        WinCanvas.gameObject.SetActive(false);
        Prologue_AfterMainMenu.gameObject.SetActive(true);
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
    public void Back()
    {
        if (GameManager.Instance.State == GameState.Puase)
        {
            PuaseCanvas.gameObject.SetActive(true);
        }
        else if (GameManager.Instance.State == GameState.Starting)
        {
            StartCanvas.gameObject.SetActive(true);
        }
        SettingPauseCanvas.gameObject.SetActive(false);
    }

    public void hidestartCanvas()
    {
        Prologue_AfterMainMenu.gameObject.SetActive(false);
    }
}
