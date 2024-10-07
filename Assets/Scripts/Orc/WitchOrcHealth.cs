using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchOrcHealth : MonoBehaviour
{

    [Header("Health")]
    public float health;

    [Header("Others")]
    Collider2D collider;
    Animator ani;

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
            this.gameObject.GetComponent<WitchOrc>().SetOrcDead();
            ani.SetTrigger("isDead");
            return;
        }
    }

    public void TakeDamage(float value)
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
