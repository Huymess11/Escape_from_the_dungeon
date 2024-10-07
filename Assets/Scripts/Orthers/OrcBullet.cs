using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcBullet : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")|| collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
