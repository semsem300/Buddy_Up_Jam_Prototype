using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] Transform playerTransform;
    Rigidbody2D rb;
    Animator animator;
    [SerializeField] float damageforce = 50f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //GameManager.Instance.ChangePattern(0);
    }
    private void Update()
    {

        if (GameManager.Instance.State == GameState.Playing)
        {

        }

    }
    public void TakeDamage(int amount)
    {
        AudioManager.Instance.PlaySoundFxSource(enemy.MonsterReceiveDamageClip);
        enemy.TakeDamage(amount);
        if (!enemy.isAlive)
        {
            StartCoroutine(Death(enemy.deathtime));
        }
        //  rb.AddForce(-rb.velocity * damageforce, ForceMode2D.Impulse);
        StartCoroutine(EnemyKnockback(1, damageforce, playerTransform));
        animator.SetTrigger("Hurt");
        if (enemy.currentHealth == 70)
        {
            GameManager.Instance.ChangePattern(1);

        }
        else if (enemy.currentHealth == 40)
        {
            GameManager.Instance.ChangePattern(2);
        }
    }
    IEnumerator Death(float time)
    {
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(time);
        GameManager.Instance.ChangePattern(3);
        yield return new WaitForSeconds(10);
        GameManager.Instance.ChangeState(GameState.Win);
    }
    public IEnumerator EnemyKnockback(float knockBackDuration, float knockBackPower, Transform obj)
    {
        float timer = 0;
        while (knockBackDuration > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - transform.position).normalized;
            rb.AddForce(-direction * knockBackPower);
        }
        yield return 0;
    }

}
