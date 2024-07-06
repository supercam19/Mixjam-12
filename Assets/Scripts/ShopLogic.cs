using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopLogic : MonoBehaviour {
    private PlayerBehavior player;
    private Inventory inventory;

    private AudioClip purchase;
    private AudioClip error;

    public Text ammoText;
    public Text healthTonicText;
    
    void Start() {
        inventory = GameInfo.inventory;
        purchase = Resources.Load<AudioClip>("SFX/purchase");
        error = Resources.Load<AudioClip>("SFX/error");
        GameInfo.money = 100;
    }

    public void BuyAmmo() {
        if (GameInfo.money >= 0.35f) {
            GameInfo.money -= 0.35f;
            inventory.ChangeCount(0, 10);
            SoundPlayer.Play(purchase);
        }
        else {
            SoundPlayer.Play(error);
        }
    }

    public void BuyHealthTonic() {
        if (GameInfo.money >= 0.75f) {
            GameInfo.money -= 0.75f;
            inventory.ChangeCount(2, 1);
            SoundPlayer.Play(purchase);
        }
        else {
            SoundPlayer.Play(error);
        }
    }
    
}
