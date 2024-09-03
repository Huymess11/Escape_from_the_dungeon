 using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [Header("Singleton")]
    public static Player instance;

    [Header("State")]
    public  bool isShield;


    [Header("Move")]
    public float speed;
    private float moveX, moveY;
    Vector2 lastMoveDirection;
    Vector2 moveMent;

    [Header("Dashing")]
    public float dashPower;
    public float dashTime;
    public float dashCoolDown;
    private bool isDashing;
    private bool canDash = true;
    public TrailRenderer tr;

    [Header("Dead")]
    private bool isDead = false;

    
    [Header("Orthers")]
    private bool isAttack;
    private bool isDmg;
    private Rigidbody2D rb;
    private Animator ani;

    private void Awake()
    {
        Player.instance = this;
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }
    void Start()
    {

    }
    void Update()
    {
        if(isDead) return;
        if (isDmg) return;
        if (isDashing) return;
        Move();
        Attack();
        Dash();
    }
    public void SetPlayerDead()
    {
        isDead = true;
    }
    private void Dash()
    {
        if(Input.GetKeyDown(KeyCode.X) && canDash)
        {
            StartCoroutine(PlayerDash());
        }
    }
    private void Move()
    {
        if (isDead) return;
        if (isAttack) return;
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        moveMent = new Vector2(moveX, moveY).normalized;
        rb.velocity = new Vector2(moveMent.x*speed,moveMent.y*speed);
        ani.SetFloat("horizontal", moveMent.x);
        ani.SetFloat("vertical",moveMent.y);
        if(moveMent != Vector2.zero )
        {
            lastMoveDirection = moveMent;
            ani.SetFloat("lastHorizontal", moveMent.x);
            ani.SetFloat("lastVertical",moveMent.y);
        }
        
    }
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAttack)
        {
            StartCoroutine(PlayerAttack());
        }
    }
    IEnumerator PlayerAttack()
    {
        rb.velocity = Vector2.zero;
        ani.SetBool("attack", true);
        isAttack = true;
        yield return null;
        ani.SetBool("attack", false);
        yield return new WaitForSeconds(0.33f);
        isAttack = false;

    }
    IEnumerator PlayerDmg()
    {
        rb.velocity = Vector2.zero;
        ani.SetBool("dmg", true);
        isDmg = true;
        yield return null;
        ani.SetBool("dmg", false);
        yield return new WaitForSeconds(0.33f);
        isDmg = false;
    }
    IEnumerator PlayerDash()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(lastMoveDirection.x * dashPower, lastMoveDirection.y * dashPower);
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        tr.emitting = false;
        rb.velocity = new Vector2(0f, 0f);
        isDashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("OrcHitbox"))
        {
            if (isShield == false) 
            {
              PlayerHealth.instance.TakeDamage(25);
              StartCoroutine(PlayerDmg());
            }
            else if(isShield == true)
            {
                isShield = false;
            }
        }
    }
}
