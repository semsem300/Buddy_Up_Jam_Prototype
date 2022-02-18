using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] Enemy enemy;
   
    Animator animator;
    private void Awake()
    {
        
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
        enemy.TakeDamage(amount);
        // animator.SetTrigger("Hurt");
    }
    IEnumerator Death(float time)
    {
        //  animator.SetTrigger("Death");
        yield return new WaitForSeconds(time);
        GameManager.Instance.ChangeState(GameState.Win);
    }
}
