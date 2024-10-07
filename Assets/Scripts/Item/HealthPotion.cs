using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public void ReHealth()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
        PlayerHealth.Instance.ReHealth();
        Destroy(gameObject);
    }
}
