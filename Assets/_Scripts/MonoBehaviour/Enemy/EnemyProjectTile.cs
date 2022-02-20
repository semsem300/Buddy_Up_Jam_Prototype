using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyProjectTile : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] Player player;
    [SerializeField] float lifetime = 0.5f;
    [SerializeField] float speed = 3f;
    Color flashColour = new Color(255f, 255f, 255f, 0.5f);
    public Image damageIndicator;
    Rigidbody2D rb;
    Vector3 target;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = player.position;
    }
    private void Start()
    {
        // Rotation
        Vector2 lookDir = new Vector2(target.x - rb.position.x, target.y - rb.position.y);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        Destroy(gameObject, lifetime);

    }
    private void Update()
    {
        rb.position = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(enemy.Pattern2AttackDamage);
            damageIndicator.color = flashColour;
            StartCoroutine(RestIamageIndicatorColor());
        }
    }

    IEnumerator RestIamageIndicatorColor()
    {
        yield return new WaitForSeconds(lifetime);
        damageIndicator.color = new Color(255f, 255f, 255f, 0); ;
    }
}
