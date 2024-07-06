using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class ZombieBehavior : MonoBehaviour {
    private GameObject player;
    private Transform target;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    private Transform healthBar;
    private Transform healthBarFill;
    private PlayerBehavior pb;

    private float speed = 0.05f;
    private bool alerted = false;
    private bool attacking = false;
    private float alertDistance = 4.0f;
    private int health = 100;
    private int damage = 2;
    private int sightRange = 10;
    private float attackRange = 1.5f;
    private float attackSpeed = 2.2f;

    private float lastTickTime = 0.0f;
    private float lastAttackTime = 0.0f;
    private Tilemap obstacles;
    public TileBase[] hazards;
    private bool onHazard = false;

    private GameObject dropPrefab;
    private Item[] drops;
    
    private float heightLimit = 1.5f;
    private float heightBottom = -4.5f;
    
    private AudioClip[] hitSounds = new AudioClip[2];

    private AudioClip[] attackSounds = new AudioClip[2];
    
    void Start() {
        player = GameObject.Find("Player");
        target = player.transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        pb = player.GetComponent<PlayerBehavior>();
        
        hitSounds[0] = Resources.Load<AudioClip>("SFX/zombie_hit");
        hitSounds[1] = Resources.Load<AudioClip>("SFX/zombie_hit_3");
        
        attackSounds[0] = Resources.Load<AudioClip>("SFX/zombie_attack");
        attackSounds[1] = Resources.Load<AudioClip>("SFX/zombie_hit_2");
        
        rb.freezeRotation = true;
        lastTickTime = Random.Range(0.0f, 0.1f);
        healthBar = transform.GetChild(0);
        healthBarFill = healthBar.GetChild(0);
        obstacles = GameObject.Find("Obstacles").GetComponent<Tilemap>();
        dropPrefab = Resources.Load<GameObject>("Prefabs/GroundItem");
        LoadDropTable();
    }

    void Update() {
        if (Time.time - 0.1 > lastTickTime) {
            Tick();
            lastTickTime = Time.time;
        }

        if (alerted) {
            if (!attacking) {
                Vector2 direction = target.position - transform.position;
                sr.flipX = direction.x > 0;
                direction = direction.normalized * speed;
                animator.SetBool("moving", true);
                rb.MovePosition((Vector2)transform.position + direction);
                if (transform.position.y > heightLimit)
                    transform.position = new Vector3(transform.position.x, heightLimit, transform.position.z);
                else if (transform.position.y < heightBottom)
                    transform.position = new Vector3(transform.position.x, heightBottom, transform.position.z);
            }
        }
    }

    private void Tick() {
        alerted = false;
        attacking = false;
        float distance = Vector2.Distance(target.position, transform.position);
        if (distance > sightRange) {
            animator.SetBool("moving", false);
        }
        else if (distance > attackRange) {
            alerted = true;
        }
        else if (Time.time - attackSpeed > lastAttackTime) {
            animator.SetBool("moving", false);
            animator.SetBool("attacking", true);
            alerted = true;
            attacking = true;
            lastAttackTime = Time.time;
            SoundPlayer.PlayRandomPitched(attackSounds, 0.2f);
            Invoke(nameof(Attack), 1);
        }

        if (onHazard)
            TakeDamage(2);
    }

    private void Attack() {
        if (Vector2.Distance(transform.position, target.position) < attackRange) {
            pb.TakeDamage(damage);
        }
        animator.SetBool("attacking", false);
    }

    public void TakeDamage(int damage) {
        healthBar.gameObject.SetActive(true);
        health -= damage;
        healthBarFill.localScale = new Vector3(-health / 100.0f, 1, 1);
        if (health <= 0)
            Die();
        else if (damage > 2) {
            SoundPlayer.PlayRandomPitched(hitSounds, 0.2f);
        }
    }

    public void Die() {
        SoundPlayer.Play(Resources.Load<AudioClip>("SFX/zombie_death"));
        GenerateDrop();
        GameInfo.ZombieKilled();
        Destroy(gameObject);
    }

    private GameObject GenerateDrop() {
        GameObject parent = new GameObject("Drop");
        parent.transform.position = transform.position;
        int rolls = Random.Range(0, 5);
        for (int i = 0; i < rolls; i++) {
            if (Utils.RandomChance(30)) {
                Vector3 displacement = new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), 0);
                GameObject drop = Instantiate(dropPrefab, transform.position + displacement, Quaternion.identity);
                drop.transform.parent = parent.transform;
                drop.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
                GroundItemBehavior gib = drop.GetComponent<GroundItemBehavior>();
                gib.item = drops[Random.Range(0, drops.Length - 1)];
                Debug.Log(gib.item.ToString());
                gib.GetComponent<SpriteRenderer>().sprite = Item.GetSprite(gib.item.id);
            }
        }

        return parent;
    }

    private void LoadDropTable() {
        string[] lines = Resources.Load<TextAsset>("zombie_droptable").text.ToString().Split("\n");
        drops = new Item[lines.Length];
        for (int i = 0; i < lines.Length; i++) {
            string[] parts = lines[i].Split(";");
            drops[i] = new Item(int.Parse(parts[0]), int.Parse(parts[0]).ToString());
            drops[i].count = int.Parse(parts[1]);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        onHazard = other.gameObject.CompareTag("Hazard");
    }
    
    void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Hazard"))
            onHazard = false;
    }
    
}
