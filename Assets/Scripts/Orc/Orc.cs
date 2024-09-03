 using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Orc : MonoBehaviour
{
    [Header("Singleton")]
    public static Orc instance;

    [Header("Idle")]
    private Transform target;


    [Header("Walk")]
    public float speed;
    public float maxDis;
    public float minDis;


    [Header("Attack")]
    private bool isAttack;

    [Header("Dmg")]
    private bool isDmg;

    [Header("Dead")]
    private bool isDead = false;

    [Header("Orther")]
    Vector2 Home;
    private Animator ani;
    private Collider2D collider;
    private Rigidbody2D rb;

    private void Awake()
    {
        Orc.instance = this;
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        ani = GetComponent<Animator>();
    }
    void Start()
    {
        target = FindObjectOfType<Player>().transform;
        Home = transform.position;
    }


    void Update()
    {
        if (isDead) return;
        if (isDmg) return;
        if (Vector3.Distance(target.position, transform.position) < maxDis && Vector3.Distance(target.position, transform.position) > minDis)
        {
            OrcWalking();
        }
        else if (Vector3.Distance(target.position, transform.position) > maxDis)
        {
            OrcGoHome();
        }
        else if (Vector3.Distance(target.position, transform.position) <= minDis && !isAttack)
        {
            StartCoroutine(OrcAttack());
        }
    }
    public void SetOrcDead()
    {
        isDead = true;
    }
    public IEnumerator OrcDmg()
    {
        ani.SetBool("isDmg", true);
        isDmg = true;
        Vector2 dir = (transform.position - target.position).normalized;
        rb.velocity = dir * 5;
        yield return null;
        ani.SetBool("isDmg", false);
        yield return new WaitForSeconds(0.33f);
        rb.velocity = Vector2.zero;
        isDmg = false;

    }
    IEnumerator OrcAttack()
    {
        ani.SetBool("isAttack", true);
        ani.SetBool("isMoving", false);
        isAttack = true;
        yield return null;
        ani.SetBool("isAttack", false);
        yield return new WaitForSeconds(1f);
        isAttack = false;
    }
    private void OrcGoHome()
    {
        transform.position = Vector3.MoveTowards(transform.position, Home, speed * Time.deltaTime);
        ani.SetFloat("horizontal", Home.x - transform.position.x);
        ani.SetFloat("vertical", Home.y - transform.position.y);
        if (Vector3.Distance(transform.position, Home) == 0)
        {
            ani.SetBool("isMoving", false);
        }
    }
    private void OrcWalking()
    {
        ani.SetBool("isMoving", true);
        ani.SetFloat("horizontal", target.position.x - transform.position.x);
        ani.SetFloat("vertical", target.position.y - transform.position.y);
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hitbox"))
        {
            OrcHealth.instance.TakeDamage(25);
            StartCoroutine(OrcDmg());
        }
    }
}
