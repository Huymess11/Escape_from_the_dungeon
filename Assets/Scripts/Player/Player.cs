using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Player : Singleton<Player>
{
    [Header("Singleton")]
    public static Player instance;

    [Header("State")]
    public  bool isShield;
    private bool isHaveKey;

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
    [SerializeField] private float playerDamage = 25;
    float m_playerDamage;
    [SerializeField] private GameObject keyPS;
    [SerializeField] private GameObject bubbleSheild;
    private Coroutine buffCoroutine;

    private void Awake()
    {
        Player.instance = this;
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }
    private void Start()
    {
        m_playerDamage = playerDamage;
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
        AudioManager.Instance.PlaySFX(AudioManager.Instance.attack);
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
        AudioManager.Instance.PlaySFX(AudioManager.Instance.hit);
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
    public void SetIsHaveKey(bool status)
    {
        isHaveKey = status;
        keyPS.SetActive(status);
    }
    public void SetSheild()
    {
        isShield = true;
        bubbleSheild.SetActive(true);
    }
    public void SetBuffStrength()
    {
        if (buffCoroutine != null)
        {
            StopCoroutine(buffCoroutine);
        }
        buffCoroutine = StartCoroutine(BuffStrength());
    }
    IEnumerator BuffStrength()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.upgrade);
        transform.DOScale(1.5f, 1f);
        SetDamage(playerDamage + playerDamage * 0.5f);
        yield return new WaitForSeconds(10f);
        transform.DOScale(1f, 1f);
        SetDamage(m_playerDamage);
        buffCoroutine = null;
    }
    private void SetDamage(float value)
    {
        playerDamage = value;
    }
    public float GetDamage()
    {
        return playerDamage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("OrcHitbox"))
        {
            if (isShield == false) 
            {
              PlayerHealth.instance.TakeDamage(10);
              StartCoroutine(PlayerDmg());
            }
            else if(isShield == true)
            {
                isShield = false;
                bubbleSheild.SetActive(false);
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Gate") && isHaveKey)
        {
            collision.gameObject.SetActive(false);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.unlock);
            SetIsHaveKey(false);
            GameManager.Instance.NextLevel();
        }
    }
}
