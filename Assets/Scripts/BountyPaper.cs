using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class BountyPaper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private Text text;
    private RectTransform rt;
    
    private string rewardString = "$0.00";
    private string killsString = "0";

    [NonSerialized] public float reward;
    [NonSerialized] public int goal;

    private bool hovering = false;
    private float upscale = 1;
    private const float MAX_UPSCALE = 1.2f;
    private const float UPSCALE_SPEED = 0.02f;
    
    void Start() {
        text = transform.GetComponentInChildren<Text>();
        rt = GetComponent<RectTransform>();
        string location = NameGenerator.GetName().Replace(" ", "\n ");
        GenerateQuest();
        text.text = "   BOUNTY\n\n*" + location + "\n\nx" + killsString + "\n zombies\n\n" + rewardString;
        SoundPlayer.Play(Resources.Load<AudioClip>("SFX/menu_ambience"));
    }

    void GenerateQuest() {
        goal = Random.Range(1, 6) * 5;
        reward = (float)goal * (40 + Random.Range(-5, 6)) / 100;
        rewardString = "$" + reward.ToString("0.00");
        killsString = goal.ToString();

    }

    void Update() {
        if (hovering) {
            if (upscale < MAX_UPSCALE) {
                upscale += UPSCALE_SPEED;
                rt.localScale = new Vector3(upscale, upscale, 1);
            }
        }
        else {
            if (upscale > 1) {
                upscale -= UPSCALE_SPEED;
                rt.localScale = new Vector3(upscale, upscale, 1);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        SoundPlayer.Play(Resources.Load<AudioClip>("SFX/ui_hover"));
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        hovering = false;
    }
}
