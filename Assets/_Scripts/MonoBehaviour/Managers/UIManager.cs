using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : StaticInstance<UIManager>
{
    [SerializeField] Canvas GameOverCanvas;
    [SerializeField] Canvas PuaseCanvas;
    [SerializeField] Canvas DialougeCanvas;
    [SerializeField] Canvas SettingCanvas;
    [SerializeField] Canvas StartCanvas;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            GameManager.Instance.ChangeState(GameState.Puase);
            PuaseCanvas.gameObject.SetActive(true);
        }
    }
    public void Restart()
    {
        GameOverCanvas.gameObject.SetActive(false);
        PuaseCanvas.gameObject.SetActive(false);
        SettingCanvas.gameObject.SetActive(false);
        StartCanvas.gameObject.SetActive(false);
        PuaseCanvas.gameObject.SetActive(false);
        GameManager.Instance.Restart();
    }
    public void Resume()
    {
        GameManager.Instance.ChangeState(GameState.Playing);
        GameOverCanvas.gameObject.SetActive(false);
        PuaseCanvas.gameObject.SetActive(false);
        SettingCanvas.gameObject.SetActive(false);
        StartCanvas.gameObject.SetActive(false);
        PuaseCanvas.gameObject.SetActive(false);
    }
    
}
