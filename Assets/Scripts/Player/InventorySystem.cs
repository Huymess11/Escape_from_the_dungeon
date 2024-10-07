using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : Singleton<InventorySystem>
{
    public bool[] isFull;
    public GameObject[] slot;
}
