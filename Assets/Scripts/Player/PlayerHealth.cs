using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Singleton")]
    public static PlayerHealth instance;

    [Header("Health")]
    public int health;

    [Header("Orther")]
    private Animator ani;
    private Collider2D collider;
    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        ani = GetComponent<Animator>();
        PlayerHealth.instance = this;
    }
    void Start()
    {
        
    }


    void Update()
    {
        CheckHealth();
    }
    public void TakeDamage(int value)
    {
        health -= value;
    }
    private void CheckHealth()
    {
        if(health <= 0)
        {
            Player.instance.SetPlayerDead();
            ani.SetBool("dead",true);
            collider.enabled = false;
            return;
        }
    }
}
