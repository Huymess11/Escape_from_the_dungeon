 using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Orc : MonoBehaviour
{

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
    Vector3 Home;
    private Animator ani;
    private Collider2D collider;
    private Rigidbody2D rb;
    private NavMeshAgent agent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        ani = GetComponent<Animator>();
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        target = FindObjectOfType<Player>().transform;
        Home = transform.position;
    }

    void Update()
    {
        if (isDead) return;
        if (isDmg) return;
        if (agent != null)
        {
            Vector3 pos = transform.position;
            pos.z = 0;
            transform.position = pos;
        }
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
        agent.speed = 0;
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
        AudioManager.Instance.PlaySFX(AudioManager.Instance.attack);
        isAttack = true;
        yield return null;
        ani.SetBool("isAttack", false);
        yield return new WaitForSeconds(1f);
        isAttack = false;
    }
    private void OrcGoHome()
    {
        //transform.position = Vector3.MoveTowards(transform.position, Home, speed * Time.deltaTime);
        agent.SetDestination(Home);
        ani.SetFloat("horizontal", Home.x - transform.position.x);
        ani.SetFloat("vertical", Home.y - transform.position.y);
        if (Vector3.Distance(transform.position,Home)<0.1f)
        {
            ani.SetBool("isMoving", false);
        }
    }
    private void OrcWalking()
    {
        ani.SetBool("isMoving", true);
        ani.SetFloat("horizontal", target.position.x - transform.position.x);
        ani.SetFloat("vertical", target.position.y - transform.position.y);
       // transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        agent.SetDestination(target.position);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hitbox"))
        {
            StartCoroutine(OrcDmg());
            AudioManager.Instance.PlaySFX(AudioManager.Instance.hit);
        }
    }
}
