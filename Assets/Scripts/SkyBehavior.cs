using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBehavior : MonoBehaviour {
    public float parallaxStrength = 0.1f;
    public Sprite[] skies;
    
    private Transform cameraPos;

    void Start() {
        cameraPos = GameObject.Find("Main Camera").transform;
        PickSky();
    }

    void Update() {
        transform.position = new Vector3(cameraPos.position.x - cameraPos.position.x * parallaxStrength, transform.position.y, transform.position.z);
        
    }

    public void PickSky() {
        int spriteIndex = Random.Range(0, skies.Length - 1);
        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
            sr.sprite = skies[spriteIndex];
    }
}
