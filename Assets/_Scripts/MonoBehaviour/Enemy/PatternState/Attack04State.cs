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
        Debug.Log("FourthPattern");
        enemy.currentPattern = AttackPattern.Pattern3;
    }
}
