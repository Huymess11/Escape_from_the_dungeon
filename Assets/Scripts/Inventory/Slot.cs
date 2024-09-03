using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public int i;
    void Start()
    {
        
    }
    void Update()
    {
        if(transform.childCount <= 1)
        {
            InventorySystem.instance.isFull[i] = false;
        }
    }
}
