using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemy_", menuName = "Enemys/CreateEnemy")]
public class Enemy : ScriptableObject
{
    [Range(0, 100)]
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
    public float minDetectRange = 0.75f;
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
    public Vector3 stopDistence = new Vector3(0.5f, 0.5f, 0);
    public float moveWaitTime = 1f;
    public float startMoveWaitTime = 1f;
    public bool isAlive = true;
    public AttackPattern currentPattern;
    public GameObject enemyObj;
    public Vector3 position = new Vector3(0, 0, 0);
    public LayerMask attackMask;
    //in stage 1 can only choose between attack pattern 1 and 2
    List<AttackPattern> RandomPattern1 = new List<AttackPattern>() { AttackPattern.Pattern1, AttackPattern.Pattern2 };
    //in stage 2 it can only choose between attack pattern 1 and 3
    List<AttackPattern> RandomPattern2 = new List<AttackPattern>() { AttackPattern.Pattern1, AttackPattern.Pattern3 };
    //in stage 3 it can only choose between 2 and 4
    List<AttackPattern> RandomPattern3 = new List<AttackPattern>() { AttackPattern.Pattern2, AttackPattern.Pattern4 };
    //in stage 4 it chooses randoly between all 4 of them
    List<AttackPattern> RandomPattern4 = new List<AttackPattern>() { AttackPattern.Pattern1, AttackPattern.Pattern2, AttackPattern.Pattern3, AttackPattern.Pattern4 };

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
    public void ChangeAttackPattern()
    {
        if (currentHealth <= 100 && currentHealth > 75)
            currentPattern = AttackPattern.Pattern1;
        else if (currentHealth <= 75 && currentHealth > 50)
            currentPattern = AttackPattern.Pattern2;
        else if (currentHealth <= 50 && currentHealth > 25)
            currentPattern = AttackPattern.Pattern3;
        else
            currentPattern = AttackPattern.Pattern4;
    }
    private AttackPattern GetRandomAttackPattern(AttackPattern attackPattern)
    {
        switch (attackPattern)
        {
            case AttackPattern.Pattern1:
                return (AttackPattern)Random.Range(0, RandomPattern1.Count);
            case AttackPattern.Pattern2:
                return (AttackPattern)Random.Range(0, RandomPattern2.Count);
            case AttackPattern.Pattern3:
                return (AttackPattern)Random.Range(0, RandomPattern3.Count);
            case AttackPattern.Pattern4:
                return (AttackPattern)Random.Range(0, RandomPattern4.Count);
            default:
                return (AttackPattern)Random.Range(0, RandomPattern1.Count);
        }
    }
    public void SwitchPattern(AttackPattern attackPattern)
    {
        currentPattern = attackPattern;
    }
    public void ResetEnemy()
    {
        currentHealth = 100;
        maxHealth = 100;
        currentMV = 100;
        maxMV = 100;
        speed = 5f;
        isAlive = true;
        currentPattern = AttackPattern.Pattern1;
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