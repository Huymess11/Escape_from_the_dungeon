using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Chest : MonoBehaviour
{
    [Header("Unlock")]
    private bool isUnlock;

    [Header("Orther")]
    [SerializeField] private ItemData itemData;
    public ItemType itemType;
    Animator ani;
    Collider2D c2d;


    private void Awake()
    {
        ani = GetComponent<Animator>();
        c2d  = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hitbox") && isUnlock == false)
        {
            isUnlock = true;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.open_chest);
            ani.SetTrigger("isUnlock");
            for(int i = 0; i < itemData.data.Count; i++)
            {
                if (itemData.data[i].type == itemType)
                {
                    if (itemData.data[i].type == ItemType.Key) { }
                    Instantiate(itemData.data[i].item, transform.position, Quaternion.identity);
                }
            }
          
        }
    }

}
