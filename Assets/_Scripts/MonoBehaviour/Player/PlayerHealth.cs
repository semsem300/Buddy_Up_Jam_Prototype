using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int numOfHeatrs;
    public Image[] hearts;
    public Sprite fullheart;
    public Sprite emptyheart;
    [SerializeField] Player player;
    Animator animator;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (GameManager.Instance.State == GameState.Playing)
        {
            if (!player.isAlive)
            {
                StartCoroutine(Death(player.deathtime));
            }
            else FillHearts();
        }
    }
    private void FillHearts()
    {
        if (player.currentHealth > numOfHeatrs)
        {
            player.currentHealth = numOfHeatrs;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < player.currentHealth)
            {
                hearts[i].sprite = fullheart;
                hearts[i].color = Color.red;
            }
            else
            {
                hearts[i].sprite = emptyheart;
                hearts[i].color = Color.white;
            }

            if (i < numOfHeatrs)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
    public void TakeDamage(int amount)
    {
        player.TakeDamage(amount);
        AudioManager.Instance.PlaySoundFxSource(player.hurtClip);
        animator.SetTrigger("Hurt");
    }
    IEnumerator Death(float time)
    {
        animator.SetTrigger("Death");
        AudioManager.Instance.PlaySoundFxSource(player.deathClip);
        yield return new WaitForSeconds(time);
        GameManager.Instance.ChangeState(GameState.Lose);
    }
}
