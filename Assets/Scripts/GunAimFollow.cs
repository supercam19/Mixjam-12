using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAimFollow : MonoBehaviour {
    private float accuracy = 1;

    private SpriteRenderer sr;
    private Camera cam;
    
    void Start() {
        sr = GetComponent<SpriteRenderer>();
        Cursor.visible = false;
        cam = Camera.main;
    }

    void Update() {
        
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePos2 = cam.ScreenToViewportPoint(Input.mousePosition);

        if (mousePos2.x > 0 && mousePos2.x < 1 && mousePos2.y > 0 && mousePos2.y < 1) {
            Cursor.visible = false;
        }
        else {
            Cursor.visible = true;
        }
        
        
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        transform.localScale = new Vector3(1.2f - accuracy, 1.2f - accuracy, 1.2f - accuracy);
    }
    
    public void SetAccuracy(float newAccuracy) {
        accuracy = newAccuracy;
    }
}
