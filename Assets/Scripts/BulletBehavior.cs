using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {
    [NonSerialized]
    public int damage;
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            other.gameObject.GetComponent<ZombieBehavior>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
