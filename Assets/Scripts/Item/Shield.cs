using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject shield;

    public void GetShield()
    {
        Destroy(gameObject);
        Instantiate(shield, Player.instance.transform, false);
        Player.instance.isShield = true;
    }
}
