using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject itemButton;
    Transform Bag;
    RectTransform bagRectTransform;

    void Start()
    {
        Bag = GameObject.FindGameObjectWithTag("Bag").transform;
        bagRectTransform = Bag.GetComponent<RectTransform>();
    }

    void Update()
    {
        for(int i = 0; i < InventorySystem.Instance.slot.Length; i++)
        {
            if (InventorySystem.Instance.isFull[i]== false)
            {
                Vector3 bagWorldPosition = bagRectTransform.position;
                transform.position = Vector3.MoveTowards(transform.position, bagWorldPosition, 0.3f);
                if(transform.position == Bag.position)
                {
                    InventorySystem.Instance.isFull[i] = true;
                    Destroy(gameObject);
                    Instantiate(itemButton, InventorySystem.Instance.slot[i].transform,false);
                }
                break;
            }
        }
    }
}
