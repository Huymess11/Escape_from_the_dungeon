using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : Singleton<HealthBarManager>
{
    Slider sliderHealth;

    private void Start()
    {
        sliderHealth = GetComponent<Slider>();
    }
    
    public void SetSliderHealth(float maxHealth, float curHealth)
    {
        sliderHealth.value = curHealth/maxHealth;
    }
}
