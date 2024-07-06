using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBehavior : MonoBehaviour {
    [NonSerialized] 
    public int health;
    [NonSerialized] 
    public int maxHealth;
    [NonSerialized] 
    public float money;
    [NonSerialized] 
    public float magnetism;
    [NonSerialized] 
    public Inventory inventory;

    public HealthBar healthBar;

    void Start() {
        inventory = GameInfo.inventory;
        money = GameInfo.money;
        maxHealth = GameInfo.maxHealth;
        health = GameInfo.health;
        magnetism = GameInfo.magnetism;
        healthBar.SetHealth(health, maxHealth);
        GameInfo.currentKills = 0;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E) && inventory.GetItem(2).count > 0) {
            health = maxHealth;
            GameInfo.health = health;
            healthBar.SetHealth(health, maxHealth);
            inventory.ChangeCount(2, -1);
            SoundPlayer.Play(Resources.Load<AudioClip>("SFX/drink"));
        }
    }
    
    public void TakeDamage(int damage) {
        health -= damage;
        GameInfo.health = health;
        healthBar.SetHealth(health, maxHealth);
        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
        money /= 2;
        health = maxHealth;
        LoadGameScene.LoadMenu();
    }

    void OnDestroy() {
        GameInfo.health = health;
        Cursor.visible = true;
    }
}
