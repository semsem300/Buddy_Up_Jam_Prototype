using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemy_", menuName = "Enemys/CreateEnemy")]
public class Enemy : ScriptableObject
{
    [Header("Health")]
    [Range(0, 100)]
    public float currentHealth = 100;
    [Range(0, 100)]
    public float maxHealth = 100;
    [Range(0, 10)]
    public float deathtime = 1f;
    public bool isAlive = true;

    [Header("Speed")]
    [Range(0, 100)]
    public float speed = 5f;
    [Range(0, 100)]
    public float maxSpeed = 10f;
    [Header("Detect Range")]
    [Range(0, 10)]
    public float maxDetectRange = 3f;
    [Range(0, 10)]
    public float minDetectRange = 0.75f;
    [Range(0, 10)]
    public int damgeOnColide = 1;
    public AttackPattern currentPattern;
    public Vector3 stopDistence = new Vector3(0.1f, 0.1f, 0);
    [Header("Patterns")]
    [Range(0, 10)]
    public float attackRange = 1f;
    [Header("Pattern1")]
    [Range(0, 10)]
    public int Pattern1AttackDamage = 1;
    [Range(0, 10)]
    public float Pattern1AttackTime = 3f;
    //   [HideInInspector]
    public float currentPattern1AttackTime = 3f;

    [HideInInspector]
    public float attack01CoolTime = 1;
    [Range(0, 10)]
    public float maxAttack01Cooltime = 1;

    [Header("Petroal")]
    [HideInInspector]
    public float moveWaitTime = 1f;
    [Range(0, 10)]
    public float startMoveWaitTime = 1f;

    [Header("Pattern2")]
    [Range(0, 10)]
    public int Pattern2AttackDamage = 10;
    public GameObject attack02Obj;
    [Range(0, 10)]
    public float Pattern2AttackTime = 3f;
    // [HideInInspector]
    public float currentPattern2AttackTime = 3f;
    [HideInInspector]
    public float attack02CoolTime = 10;
    [Range(0, 10)]
    public float maxAttack02Cooltime = 10;

    [Header("Pattern3")]
    public GameObject attack03Obj;
    [Range(0, 10)]
    public int Pattern3AttackDamage = 3;
    [Range(0, 10)]
    public float Pattern3AttackTime = 3f;
    [HideInInspector]
    public float currentPattern3AttackTime = 3f;
    [HideInInspector]
    public float attack03CoolTime = 1;
    [Range(0, 10)]
    public float maxAttack03Cooltime = 1;


    [Header("Pattern4")]
    [Range(0, 10)]
    public int Pattern4AttackDamage = 4;
    [Range(0, 10)]
    public float Pattern4AttackTime = 3f;
    [HideInInspector]
    public float currentPattern4AttackTime = 3f;
    [HideInInspector]
    public float attack04CoolTime = 1;
    [Range(0, 10)]
    public float maxAttack04Cooltime = 1;

    [Header("Audios")]
    public AudioClip movingClip;

    [Header("Boss Info")]
    public GameObject enemyObj;
    public Vector3 position = new Vector3(0, 0, 0);
    public LayerMask attackMask;

    public bool canShoot = false;
    //////in stage 1 can only choose between attack pattern 1 and 2
    ////List<AttackPattern> RandomPattern1 = ;
    ////in stage 2 it can only choose between attack pattern 1 and 3
    //List<AttackPattern> RandomPattern2 = 
    ////in stage 3 it can only choose between 2 and 4
    //List<AttackPattern> RandomPattern3 = new List<AttackPattern>() {  };
    ////in stage 4 it chooses randoly between all 4 of them
    //List<AttackPattern> RandomPattern4 = new List<AttackPattern>() ;

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
        Debug.Log("Change Attack Pattern To");
        //TODO Change values to GetRandomAttackPattern(currentPattern)
        if (currentHealth <= 100 && currentHealth > 75)
        {
            currentPattern = GetRandomAttackPattern(AttackPattern.Pattern1);
        }
        else if (currentHealth <= 75 && currentHealth > 50)
        {
            currentPattern = GetRandomAttackPattern(AttackPattern.Pattern2);
        }
        else if (currentHealth <= 50 && currentHealth > 25)
        {
            currentPattern = GetRandomAttackPattern(AttackPattern.Pattern3);
        }
        else
        {
            currentPattern = GetRandomAttackPattern(AttackPattern.Pattern4);
        }
        Debug.Log(currentPattern.ToString());
    }
    private AttackPattern GetRandomAttackPattern(AttackPattern attackPattern)
    {
        switch (attackPattern)
        {
            case AttackPattern.Pattern1:
                return (AttackPattern)(Random.Range(0, new List<AttackPattern>() { AttackPattern.Pattern1, AttackPattern.Pattern3 }.Count) + 1);
            case AttackPattern.Pattern2:
                return (AttackPattern)(Random.Range(0, new List<AttackPattern>() { AttackPattern.Pattern2, AttackPattern.Pattern3 }.Count) + 1);
            case AttackPattern.Pattern3:
                return (AttackPattern)(Random.Range(0, new List<AttackPattern>() { AttackPattern.Pattern2, AttackPattern.Pattern3, AttackPattern.Pattern4 }.Count) + 1);
            case AttackPattern.Pattern4:
                return (AttackPattern)(Random.Range(0, new List<AttackPattern>() { AttackPattern.Pattern1, AttackPattern.Pattern2, AttackPattern.Pattern3, AttackPattern.Pattern4 }.Count) + 1);
            default:
                return (AttackPattern)(Random.Range(0, new List<AttackPattern>() { AttackPattern.Pattern1, AttackPattern.Pattern2, AttackPattern.Pattern3, AttackPattern.Pattern4 }.Count) + 1);
        }
    }
    public void SwitchPattern(AttackPattern attackPattern)
    {
        currentPattern = attackPattern;
    }
    public void ResetEnemy()
    {
        currentHealth = 49;
        maxHealth = 100;
        speed = 5f;
        isAlive = true;
        currentPattern = AttackPattern.Pattern3;
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