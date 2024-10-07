using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WitchOrc : MonoBehaviour
{

    [Header("Idle")]
    private Transform target;


    [Header("Walk")]
    public float maxDis;
    public float minDis;


    [Header("Attack")]
    private bool isAttack;

    [Header("Dmg")]
    private bool isDmg;

    [Header("Dead")]
    private bool isDead = false;

    [Header("Orther")]
    private Animator ani;
    private NavMeshAgent agent;
    [SerializeField] private GameObject bulletPrefab;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        target = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        if (isDead) return;
        if (isDmg) return;
        if (Vector3.Distance(target.position, transform.position) < maxDis )
        {
            if (target.position.x > transform.position.x)
            {
                transform.localScale = new Vector3( 1, 1, 1);
            }
            else if(target.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        if (Vector3.Distance(target.position, transform.position) <= minDis && !isAttack)
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
        yield return null;
        ani.SetBool("isDmg", false);
        yield return new WaitForSeconds(0.33f);
        isDmg = false;

    }
    IEnumerator OrcAttack()
    {
        
        isAttack = true;
        GameObject bullet = Instantiate(bulletPrefab, transform.position,Quaternion.identity );
        bullet.transform.up = target.position - transform.position;
        yield return new WaitForSeconds(3f);
        isAttack = false;
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
