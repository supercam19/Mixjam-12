using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtons : MonoBehaviour
{
    public GameObject mainScreen;
    public GameObject shopScreen;
    public GameObject settingsScreen;
    
    public void LoadSettingsMenu() {
        mainScreen.SetActive(false);
        shopScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }
    
    public void LoadShopMenu() {
        mainScreen.SetActive(false);
        settingsScreen.SetActive(false);
        shopScreen.SetActive(true);
    }
    
    public void LoadMainMenu() {
        shopScreen.SetActive(false);
        settingsScreen.SetActive(false);
        mainScreen.SetActive(true);
    }

    public void LoadGame1() {
        LoadGameScene.LoadGame(0);
    }
    
    public void LoadGame2() {
        LoadGameScene.LoadGame(1);
    }
    
    public void LoadGame3() {
        LoadGameScene.LoadGame(2);
    }
}
