using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    [SerializeField] Player player;
    protected override void Update()
    {
        if (!player.isAlive)
        {
            player.ResetPlayerHealth();
            SceneManager.LoadScene(0);
        }
        base.Update();
    }
}
