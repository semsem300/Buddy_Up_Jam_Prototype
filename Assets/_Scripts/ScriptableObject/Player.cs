using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player_", menuName = "Players/CreatePlayer")]
public class Player : ScriptableObject
{
    public float currentHealth = 100;
    public float maxHealth = 100;

    public float currentMV = 100;
    public float maxMV = 100;

    public float speed = 5f;
    public float maxSpeed = 10f;

    public float jumpForce = 500f;
    public float maxJumpForce = 1000f;

    public GameObject playerObj;
    public bool isAlive = true;

    public void AddDamage(float amount)
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
    public void ResetPlayer()
    {
    }
}
