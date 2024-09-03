using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Unlock")]
    private bool isUnlock;

    [Header("Orther")]
    public GameObject item;
    Animator ani;
    Collider2D c2d;


    private void Awake()
    {
        ani = GetComponent<Animator>();
        c2d  = GetComponent<Collider2D>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hitbox") && isUnlock == false)
        {
            isUnlock = true;
            ani.SetTrigger("isUnlock");
            Instantiate(item, transform.position, Quaternion.identity);
        }
    }

}
