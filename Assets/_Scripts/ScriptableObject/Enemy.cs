using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemy_", menuName = "Enemys/CreateEnemy")]
public class Enemy : ScriptableObject
{
    [Range(0,100)]
    public float currentHealth = 100;
    [Range(0, 100)]
    public float maxHealth = 100;
    [Range(0, 100)]
    public float currentMV = 100;
    [Range(0, 100)]
    public float maxMV = 100;
    [Range(0, 100)]
    public float speed = 5f;
    [Range(0, 100)]
    public float maxSpeed = 10f;
    [Range(0, 10)]
    public float maxDetectRange = 3f;
    [Range(0, 10)]
    public float minDetectRange=0.75f;
    [Range(0, 10)]
    public int Pattern1AttackDamage = 1;
    [Range(0, 10)]
    public int Pattern2AttackDamage = 2;
    [Range(0, 10)]
    public int Pattern3AttackDamage = 3;
    [Range(0, 10)]
    public int Pattern4AttackDamage = 4;
    [Range(0, 10)]
    public float attackRange = 1f;
    [Range(0, 10)]
    public float deathtime = 1f;
    public Vector3 stopDistence = new Vector3(1, 1, 0);
    public float moveWaitTime = 1f;
    public float startMoveWaitTime = 1f;
    public bool isAlive = true;
    public AttackPattern currentPattern;
    
    public LayerMask attackMask;
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
    public void SwitchPattern(AttackPattern attackPattern)
    {
        currentPattern = attackPattern;
    }
    void MakeDead()
    {
        isAlive = false;
    }
}
public enum AttackPattern
{
    Pattern1 = 1,
    Pattern2 = 2,
    Pattern3 = 3,
    Pattern4 = 4,
}