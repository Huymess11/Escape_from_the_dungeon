using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject itemButton;
    public Transform Bag;

    void Start()
    {
        Bag = GameObject.FindGameObjectWithTag("Bag").transform;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < InventorySystem.instance.slot.Length; i++)
        {
            if (InventorySystem.instance.isFull[i]== false)
            {
                transform.position = Vector3.MoveTowards(transform.position, Bag.position, 0.3f);
                if(transform.position == Bag.position)
                {
                    InventorySystem.instance.isFull[i] = true;
                    Destroy(gameObject);
                    Instantiate(itemButton, InventorySystem.instance.slot[i].transform,false);
                }
                break;
            }
        }
    }
}
