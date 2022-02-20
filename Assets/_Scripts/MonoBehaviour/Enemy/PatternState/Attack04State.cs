using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack04State : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FourthPatternStrategy(Animator animator, Vector2 position1, Vector3 position2)
    {
        if (enemy.currentPattern4AttackTime > 0)
        {
            Debug.Log("FourthPattern");
        }
        else if (enemy.currentPattern4AttackTime <= 0)
        {
            enemy.ChangeAttackPattern();
            enemy.currentPattern4AttackTime = enemy.Pattern4AttackTime;
        }
    }
}
