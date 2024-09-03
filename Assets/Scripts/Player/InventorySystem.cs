using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [Header("Singleton")]
    public static InventorySystem instance;
    public bool[] isFull;
    public GameObject[] slot;
    private void Awake()
    {
        InventorySystem.instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
