using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
   public void BuffStrength()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
        Destroy(gameObject);
        Player.Instance.SetBuffStrength();
    }
}
