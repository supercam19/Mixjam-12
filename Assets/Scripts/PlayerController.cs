using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed;
    public float heightLimit = 1.5f;
    public float heightBottom = -4.5f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    private bool facingLeft;
    private int levelLength;
    private AudioSource src;
    private AudioClip gallop;
    
    private static AudioClip[] gameMusic = new AudioClip[2];
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        src = GetComponent<AudioSource>();
        gallop = Resources.Load<AudioClip>("SFX/gallop");
        src.volume = GameInfo.sfxVolume * GameInfo.masterVolume;
        levelLength = GameObject.Find("World").GetComponent<GenerateLevel>().levelLength;
        speed = GameInfo.speed;
        gameMusic[0] = Resources.Load<AudioClip>("SFX/ingame_theme_1");
        gameMusic[1] = Resources.Load<AudioClip>("SFX/ingame_theme_2");
        SoundPlayer.Play(gameMusic);
    }

    
    void Update() {
        transform.rotation = Quaternion.identity;
    }

    void FixedUpdate() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        if (transform.position.y > heightLimit)
            transform.position = new Vector3(transform.position.x, heightLimit, transform.position.z);
        else if (transform.position.y < heightBottom)
            transform.position = new Vector3(transform.position.x, heightBottom, transform.position.z);
        if (transform.position.x < 0)
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        else if (transform.position.x > levelLength  && GameInfo.currentKills < GameInfo.goal)
            transform.position = new Vector3(levelLength, transform.position.y, transform.position.z); 
        else if (transform.position.x > levelLength + 2) {
            GameInfo.currentKills = 0;
            GameInfo.money += GameInfo.reward;
            LoadGameScene.LoadMenu();
        }


        if (horizontal != 0 || vertical != 0) {
            animator.SetBool("moving", true);
            if (src.clip == null) {
                src.clip = gallop;
                src.Play();
            }
        }
        else {
            animator.SetBool("moving", false);
            if (src.clip != null) {
                src.Stop();
                src.clip = null;
            }
        }

        if (horizontal < 0)
            facingLeft = true;
        else if (horizontal > 0)
            facingLeft = false;
        sr.flipX = facingLeft;
        
        // Multiply scalars first for efficiency
        rb.velocity = new Vector2(horizontal, vertical) * (speed * Time.deltaTime);
        //rb.AddForce(new Vector2(horizontal, vertical) * (speed * Time.deltaTime), ForceMode2D.Impulse);
        //rb.MovePosition((Vector2)transform.position + new Vector2(horizontal, vertical) * (speed * Time.deltaTime));

    }
}
