using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    private static GameUIManager instance;
    
    [SerializeField] private Slider healthslider;
    [SerializeField] private Slider easeHealthSlider;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }

    public static void SetHealthText(int health, Slider healthbar, Slider easeHealthBar)
    {
        healthbar.value = health;
        easeHealthBar.value = Mathf.Lerp(easeHealthBar.value, health, 0.01f);
    }
}
