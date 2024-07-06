using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraBehavior : MonoBehaviour {
    private Camera cam;
    private GenerateLevel world;
    private int worldSize;
    private GameObject player;

    private Vector3 levelStart = new Vector3(8.5f, 0.98f, -10f);

    void Start() {
        cam = GetComponent<Camera>();
        world = GameObject.Find("World").GetComponent<GenerateLevel>();
        worldSize = world.levelLength;
        player = GameObject.Find("Player");
    }
    
    void Update() {
        Vector3 leftEdge = cam.ViewportToWorldPoint(new Vector3(0, 0.5f, cam.nearClipPlane));
        float centerToEdge = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, cam.nearClipPlane)).x - leftEdge.x;
        float camX;
        if (player.transform.position.x < centerToEdge)
            camX = centerToEdge;
        else if (player.transform.position.x > worldSize - centerToEdge)
            camX = worldSize - centerToEdge;
        else
            camX = player.transform.position.x;
        cam.transform.position = new Vector3(camX, cam.transform.position.y, cam.transform.position.z);
        
        
    }
}
