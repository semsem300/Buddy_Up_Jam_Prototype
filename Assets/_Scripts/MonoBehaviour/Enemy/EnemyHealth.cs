using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    Rigidbody2D rb;
    Animator animator;
    [SerializeField] float damageforce = 50f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (GameManager.Instance.State == GameState.Playing)
        {
            if (!enemy.isAlive)
            {
                StartCoroutine(Death(enemy.deathtime));
            }
        }

    }
    public void TakeDamage(int amount)
    {
        AudioManager.Instance.PlaySoundFxSource(enemy.MonsterReceiveDamageClip);
        enemy.TakeDamage(amount);
        rb.AddForce(-rb.velocity * damageforce, ForceMode2D.Impulse);
        animator.SetTrigger("Hurt");
    }
    IEnumerator Death(float time)
    {
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(time);
        GameManager.Instance.ChangeState(GameState.Win);
    }
}
