using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundItemBehavior : MonoBehaviour {
    [NonSerialized]
    public Item item;

    private Rigidbody2D rb;
    private Transform player;
    private PlayerBehavior pb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        GameObject p = GameObject.Find("Player");
        player = p.transform;
        pb = p.GetComponent<PlayerBehavior>();
    }

    void Update() {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance < pb.magnetism) {
            Vector2 direction = player.position - transform.position;
            // scale speed up as item approaches.
            rb.MovePosition((Vector2)transform.position + direction.normalized * (pb.magnetism - distance)
                / 5);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            if (item.id == 1) {
                GameInfo.money += (float)item.count / 100;
            }
            else {
                GameInfo.inventory.ChangeCount(item.id, item.count);
            }
            if (transform.parent.childCount < 2)
                Destroy(transform.parent.gameObject);
            else
                Destroy(gameObject);
        }
    }
}
