using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Singleton<PlayerHealth>
{
    [Header("Singleton")]
    public static PlayerHealth instance;

    [Header("Health")]
    public float health;
    float curHealth;

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
        curHealth = health;
    }



    public void TakeDamage(int value)
    {
        curHealth -= value;
        HealthBarManager.Instance.SetSliderHealth(health,curHealth);
        CheckHealth();

    }
    public void ReHealth()
    {
        float bloodLost = health - curHealth;
        float rehealthBlood = curHealth * 0.25f;
        if (bloodLost < rehealthBlood)
        {
            curHealth += bloodLost;
        }
        else
        {
            curHealth += rehealthBlood;
        }
        HealthBarManager.Instance.SetSliderHealth(health, curHealth);
    }
    private void CheckHealth()
    {
        if(curHealth <= 0)
        {
            Player.instance.SetPlayerDead();
            AudioManager.Instance.PlaySFX(AudioManager.Instance.game_over);
            ani.SetBool("dead",true);
            collider.enabled = false;
            GameManager.Instance.ShowGameOverPanel();
            return;
        }
    }
}
