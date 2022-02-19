using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    [SerializeField] Canvas CreditsCanvas;
    [SerializeField] Canvas MainMenuCanvas;
    public void BackToMain()
    {
        CreditsCanvas.gameObject.SetActive(false);
        MainMenuCanvas.gameObject.SetActive(true);
        GameManager.Instance.ChangeState(GameState.Puase);
    }
}
