using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] Enemy Enemy;
    List<AttackPattern> RandomPattern1;
    List<AttackPattern> RandomPattern2;
    List<AttackPattern> RandomPattern3;
    List<AttackPattern> RandomPattern4;
    private void Awake()
    {
        //in stage 1 can only choose between attack pattern 1 and 2
        RandomPattern1 = new List<AttackPattern>() { AttackPattern.Pattern1, AttackPattern.Pattern2 };
        //in stage 2 it can only choose between attack pattern 1 and 3
        RandomPattern2 = new List<AttackPattern>() { AttackPattern.Pattern1, AttackPattern.Pattern3 }; ;
        //in stage 3 it can only choose between 2 and 4
        RandomPattern3 = new List<AttackPattern>() { AttackPattern.Pattern2, AttackPattern.Pattern4 }; ;
        //in stage 4 it chooses randoly between all 4 of them
        RandomPattern4 = new List<AttackPattern>() { AttackPattern.Pattern1, AttackPattern.Pattern2, AttackPattern.Pattern3, AttackPattern.Pattern4 }; ;
    }
    private void Start()
    {
        Enemy.currentPattern = AttackPattern.Pattern1;
    }
    private void Update()
    {
        if (GameManager.Instance.State == GameState.Playing)
        {
            if (Enemy.isAlive)
                ChangeAttackPattern();
        }
    }
    private void ChangeAttackPattern()
    {
        if (Enemy.currentHealth <= 100 && Enemy.currentHealth > 75)
            Enemy.currentPattern = GetRandomAttackPattern(AttackPattern.Pattern1);
        else if (Enemy.currentHealth <= 75 && Enemy.currentHealth > 50)
            Enemy.currentPattern = GetRandomAttackPattern(AttackPattern.Pattern4);
        else if (Enemy.currentHealth <= 50 && Enemy.currentHealth > 25)
            Enemy.currentPattern = GetRandomAttackPattern(AttackPattern.Pattern3);
        else
            Enemy.currentPattern = GetRandomAttackPattern(AttackPattern.Pattern4);
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
}
