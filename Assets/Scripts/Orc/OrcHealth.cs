using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrcHealth : MonoBehaviour
{
    [Header("Singleton")]
    public static OrcHealth instance;

    [Header("Health")]
    public float health;

    [Header("Others")]
    Collider2D collider;
    Animator ani;

    private void Awake()
    {
        OrcHealth.instance = this;
    }
    void Start()
    {
        collider = GetComponent<Collider2D>();
        ani = GetComponent<Animator>();
    }


    void Update()
    {
        if (health <= 0)
        {
            collider.enabled = false;
            this.gameObject.GetComponent<Orc>().SetOrcDead();
            ani.SetTrigger("isDead");
            return;
        }
    }

    public void TakeDamage( float value)
    {
        health -= value;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hitbox"))
        {
            TakeDamage(Player.Instance.GetDamage());
        }
    }
}
