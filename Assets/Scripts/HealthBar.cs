using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    private CharacterController2D playerHealth;

    // void Start()
    // {
    //   playerHealth = GameObject.FindObjectOfType<CharacterController2D>();
    //   playerHealth.TakeDamage();
    // }

    public void SetMaxHealth(int health)
    {
      slider.maxValue = health;
      slider.value = health;

      fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
      slider.value = health;

      fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
