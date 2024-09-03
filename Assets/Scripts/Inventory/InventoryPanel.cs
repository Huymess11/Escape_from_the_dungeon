using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [Header("Inventory")]
    bool isOpenInventory;
    Animator ani;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)&& !isOpenInventory)
        {
            isOpenInventory = true;
            ani.SetBool("isOpen",true);
        }
        else if(Input.GetKeyDown(KeyCode.C)&& isOpenInventory)
        {
            isOpenInventory = false;
            ani.SetBool("isOpen",false);
        }
    }
}
