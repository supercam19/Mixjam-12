using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Text healthText;
    private Slider healthBar;

    void Start() {
        healthBar = GetComponent<Slider>();
    }

    public void SetHealth(int health, int maxHealth) {
        if (healthBar == null) {
            healthBar = GetComponent<Slider>();
        }
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
        healthText.text = health + "/" + maxHealth;
    }
}
