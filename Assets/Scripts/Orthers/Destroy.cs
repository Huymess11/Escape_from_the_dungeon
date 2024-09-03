using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
   public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
   public void UnActiveGameObject()
    {
        gameObject.SetActive(false);
    }
}
