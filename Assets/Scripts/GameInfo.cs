using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfo : MonoBehaviour {
    public BountyPaper[] bounties;
    public static Text goalText;

    public static float reward;
    public static int goal;

    public static Inventory inventory;

    public static float masterVolume = 1;
    public static float musicVolume = 0.5f;
    public static float sfxVolume = 0.5f;

    public static int currentKills = 0;

    // Changeable stats
    public static int maxHealth = 20;
    public static int health = 20;
    public static float speed = 3000.0f;
    public static float magnetism = 2.5f;
    public static float money = 0;
    public static float fireRate = 0.7f;
    public static int damage = 30;
    public static float reloadSpeed = 0.6f;


    void Start() {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(this);
        inventory = GetComponent<Inventory>();
        SoundPlayer.Play(Resources.Load<AudioClip>("SFX/intro"));
    }

    void Update() {
        Text t = GameObject.Find("MoneyCountText").GetComponent<Text>();
        t.text = "$" + money.ToString("0.00");
        if (GameObject.Find("TonicCountText") != null) {
            Text t2 = GameObject.Find("TonicCountText").GetComponent<Text>();
            if (t2 != null)
                t2.text = "x" + inventory.GetItem(2).count;
        }
    }

    public void SaveBountyInfo(int index) {
        BountyPaper bounty = GameObject.Find("Bounty " + (index + 1)).GetComponent<BountyPaper>();
        currentKills = -1;
        reward = bounty.reward;
        goal = bounty.goal;
    }

    public static void ZombieKilled() {
        currentKills++;
        money += Random.Range(0, 0.15f);
        goalText = GameObject.Find("Goal").GetComponent<Text>();
        if (currentKills < goal)
            goalText.text = "Kill " + currentKills + "/" + goal + " Zombies!";
        else if (currentKills == goal) {
            // once only
            SoundPlayer.Play(Resources.Load<AudioClip>("SFX/bounty_complete"));
            goalText.text = "Reach the end!";
            goalText.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("assets_30");
        }
        
    }
}
