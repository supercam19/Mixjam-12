using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUpgradeManager : MonoBehaviour {
     public Text buttonText;
     public float cost;
     public float costScaleOnUpgarde = 1.5f;
     public int maxUpgrades = 5;
     public bool changePercent = false;
     public float damageChange;
     public float maxHealthChange;
     public float speedChange;
     public float magnetismChange;
     public float reloadSpeedChange;
     public float attackSpeedChange;
     
     private Slider bar;
     private int upgrades = 0;

     private AudioClip purchase;
     private AudioClip error;

     void Start() {
          bar = GetComponent<Slider>();
          bar.maxValue = maxUpgrades;
          purchase = Resources.Load<AudioClip>("SFX/purchase");
          error = Resources.Load<AudioClip>("SFX/error");
     }

     public void OnPurchaseButton() {
          if (upgrades < maxUpgrades && GameInfo.money >= cost) {
               GameInfo.money -= cost;
               if (!changePercent)
                    UpdateStats();
               else 
                    UpdateStatsPercent();
               upgrades++;
               bar.value = upgrades;
               cost *= costScaleOnUpgarde;
               if (upgrades < maxUpgrades)
                    buttonText.text = "+ $" + cost.ToString("0.00");
               else
                    buttonText.text = "MAX";
               SoundPlayer.Play(purchase);
          }
          else {
               SoundPlayer.Play(error);
          }
     }

     private void UpdateStats() {
          GameInfo.damage += (int)damageChange;
          GameInfo.maxHealth += (int)maxHealthChange;
          GameInfo.speed += speedChange;
          GameInfo.magnetism += magnetismChange;
          GameInfo.reloadSpeed -= reloadSpeedChange;
          GameInfo.fireRate -= attackSpeedChange;
     }

     private void UpdateStatsPercent() {
          GameInfo.damage = (int)Math.Round(GameInfo.damage * (1.0f + damageChange));
          GameInfo.maxHealth = (int)Math.Round(GameInfo.maxHealth * (1 + maxHealthChange));
          GameInfo.speed *= 1 + speedChange;
          GameInfo.magnetism *= 1 + magnetismChange;
          GameInfo.reloadSpeed *= 1 - reloadSpeedChange;
          GameInfo.fireRate *= 1 - attackSpeedChange;
     }
}
