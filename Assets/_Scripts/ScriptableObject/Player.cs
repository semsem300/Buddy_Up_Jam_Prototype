using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player_", menuName = "Players/CreatePlayer")]
public class Player : ScriptableObject
{
    [Range(0, 10)]
    public float currentHealth = 10;
    [Range(0, 10)]
    public float maxHealth = 10;
    [Range(0, 100)]
    public float currentMV = 100;
    [Range(0, 100)]
    public float maxMV = 100;
    [Range(0, 100)]
    public float speed = 20f;
    [Range(0, 100)]
    public float maxSpeed = 20f;
    [Range(0, 10)]
    public float attackRange;
    [Range(0, 100)]
    public int damage;
    [Range(0, 100)]
    public float dashDistance = 15f;
    [Range(0, 10)]
    public float dashtime = 0.4f;
    [Range(0, 10)]
    public float dashCoolTime = 0.9f;
    [Range(0, 10)]
    public float deathtime = 1f;
    public float attackCoolTime = 10;
    public float maxAttackCooltime = 10;
    public GameObject playerObj;
    public Vector3 position = new Vector3(2, 2, 0);
    // public GameObject dashParticle;
    public bool isAlive = true;
    public bool isDash = false;
    public LayerMask attackMask;
    public int FinalScore { get; set; }

    public void TakeDamage(float amount)
    {
        if (currentHealth > amount)
        {
            currentHealth -= amount;
            if (currentHealth <= 0)
                MakeDead();
        }
        else MakeDead();
    }
    public void AddHealth(float ammount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += ammount;
            if (currentHealth >= maxHealth)
                currentHealth = maxHealth;
        }
    }
    public void MakeDead()
    {
        isAlive = false;
    }
    public void ResetPlayerHealth()
    {
        currentHealth = 10;
        maxHealth = 10;
        isAlive = true;
    }
}
