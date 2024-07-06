using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameScene : MonoBehaviour {
    
    public static void LoadGame(int index) {
        SoundPlayer.Play(Resources.Load<AudioClip>("SFX/bounty_select"));
        GameObject.Find("GameInfo").GetComponent<GameInfo>().SaveBountyInfo(index);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public static void LoadMenu() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
