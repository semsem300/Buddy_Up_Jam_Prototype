using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyRay : MonoBehaviour
{

    [SerializeField] Enemy enemy;
    [SerializeField] Player player;
    Color flashColour = new Color(255f, 255f, 255f, 0.5f);
    public Image damageIndicator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(enemy.Pattern3AttackDamage);
            damageIndicator.color = flashColour;
            StartCoroutine(RestIamageIndicatorColor());
        }
    }

    IEnumerator RestIamageIndicatorColor()
    {
        yield return new WaitForSeconds(0.5f);
        damageIndicator.color = new Color(255f, 255f, 255f, 0); ;
    }
}
