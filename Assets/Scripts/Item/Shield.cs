using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public void GetShield()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
        Destroy(gameObject);
        Player.Instance.SetSheild();
    }
}
