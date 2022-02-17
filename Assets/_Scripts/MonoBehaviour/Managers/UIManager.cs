using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
   [SerializeField] Canvas GameOverCanvas;
   [SerializeField] Canvas PuaseCanvas;
   [SerializeField] Canvas DialougeCanvas;
   [SerializeField] Canvas SettingCanvas;
   [SerializeField] Canvas StartCanvas;
   [SerializeField] Canvas WinCanvas;

    protected override void Update()
    {
        base.Update();
    }
}
