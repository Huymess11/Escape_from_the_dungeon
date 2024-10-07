using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
   public void GetKey()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
        Destroy(gameObject);
        Player.Instance.SetIsHaveKey(true);
    }
}
