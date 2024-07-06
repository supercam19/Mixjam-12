using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunType : MonoBehaviour {
    [NonSerialized]
    public const int REVOLVER_AMMO = 0;
    [NonSerialized]
    public const int SHOTGUN_AMMO = 1;
    [NonSerialized]
    public const int RIFLE_AMMO = 2;
    [NonSerialized]

    public const int INDIVIDUAL_RELOAD = 0;
    [NonSerialized]
    public const int BATCH_RELOAD = 1;
    [NonSerialized]
    
    protected int damage = 1;
    protected float fireRate = 1.0f;
    protected int maxAmmo = 10;
    protected int loadedAmmo = 10;
    protected int ammoType = REVOLVER_AMMO;
    protected int reloadType = INDIVIDUAL_RELOAD;
    protected float reloadTime = 1.0f;
    protected float accuracyMax = 1.0f;
    protected float accuracyMin = 0.25f;
    protected float focusSpeed = 0.01f;
    protected float bulletSpeed = 20.0f;
    protected AudioClip[] fireSound;
    protected AudioClip[] reloadSound;
    protected float pitchVariance = 0;
    
    private float accuracy = 1.0f;
    private float lastFireTime;
    protected bool reloading = false;
    private float reloadStartTime;

    private Text ammoText;
    private GunAimFollow aimReticle;
    private GameObject bulletPrefab;
    private Rigidbody2D player;
    
    
    void Start() {
        ammoText = GameObject.Find("AmmoCount").GetComponent<Text>();
        aimReticle = GameObject.Find("AimArea").GetComponent<GunAimFollow>();
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        ammoText.text = loadedAmmo + "/" + GameInfo.inventory.GetItem(0).count;
        player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        Initialize();
    }

    protected virtual void Initialize() {
    }

    public virtual void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Fire();
            // interrupt reload
            reloading = false;
        }
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Q) || loadedAmmo == 0) {
            if (loadedAmmo < maxAmmo && !reloading) {
                reloading = true;
                reloadStartTime = Time.time;
            }
        }

        if (reloading) {
            if (Time.time - reloadTime > reloadStartTime) {
                ReloadComplete();
            }
        }

        if (accuracy < accuracyMax) {
            accuracy += focusSpeed;
            aimReticle.SetAccuracy(accuracy);
        }
        ammoText.text = loadedAmmo + "/" + GameInfo.inventory.GetItem(0).count;
    }

    protected virtual void Fire() {
        if (loadedAmmo > 0) {
            if (Time.time - fireRate > lastFireTime) {
                lastFireTime = Time.time;
                loadedAmmo--;
                
                
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(1 - accuracy, 1 - accuracy, 0) - transform.position;
                direction = direction.normalized * bulletSpeed;
                Rigidbody2D rbb = bullet.GetComponent<Rigidbody2D>();
                // y movement is not fun
                rbb.velocity = new Vector2(player.velocity.x, 0);
                rbb.AddForce(direction, ForceMode2D.Impulse);
                bullet.GetComponent<BulletBehavior>().damage = damage;
                
                accuracy = accuracyMin;
                aimReticle.SetAccuracy(accuracy);
                if (fireSound != null)
                    SoundPlayer.PlayRandomPitched(fireSound, pitchVariance);
            }
        }
    }

    protected virtual void ReloadComplete() {
        if (reloadType == BATCH_RELOAD) {
            int ammo = GameInfo.inventory.GetItem(0).count;
            int neededAmmo = maxAmmo - loadedAmmo;
            if (ammo >= neededAmmo) {
                GameInfo.inventory.ChangeCount(0, -neededAmmo);
                loadedAmmo = maxAmmo;
                if (reloadSound != null) 
                    SoundPlayer.PlayRandomPitched(reloadSound, pitchVariance);
            }
            else {
                GameInfo.inventory.ChangeCount(0, -ammo);
                loadedAmmo += ammo;
                if (reloadSound != null) 
                    SoundPlayer.PlayRandomPitched(reloadSound, pitchVariance);
            }

            reloading = false;
        }
        else if (reloadType == INDIVIDUAL_RELOAD) {
            if (GameInfo.inventory.GetItem(0).count > 0 && loadedAmmo < maxAmmo) {
                GameInfo.inventory.ChangeCount(0, -1);
                loadedAmmo++;
                reloadStartTime = Time.time;
                if (reloadSound != null) 
                    SoundPlayer.PlayRandomPitched(reloadSound, pitchVariance);
            }
            else {
                reloading = false;
            }
        }
        
    }
}
