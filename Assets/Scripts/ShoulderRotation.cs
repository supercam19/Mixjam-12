using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderRotation : MonoBehaviour
{
    private Camera cam;

    void Start() {
        cam = Camera.main;
    }
    void Update() {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < transform.position.x) {
            transform.rotation = Quaternion.Euler(180, 0, -GetAngle(mousePos));;
        }
        else {
            transform.rotation = Quaternion.Euler(0, 0, GetAngle(mousePos));
        }
        
    }

    private float GetAngle(Vector3 target) {
        Vector3 hyp = target - transform.position;
        return Mathf.Atan2(hyp.y, hyp.x) * Mathf.Rad2Deg;
    }
}
