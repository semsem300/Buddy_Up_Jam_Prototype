using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prologue : MonoBehaviour
{
    [SerializeField] Canvas PrologueCanvas;
    public void Continue()
    {
        PrologueCanvas.gameObject.SetActive(false);
        GameManager.Instance.ChangeState(GameState.Dialogue);
    }
}
