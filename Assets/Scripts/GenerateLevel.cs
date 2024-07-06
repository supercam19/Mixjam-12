using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateLevel : MonoBehaviour {
    public int levelLength = 150;
    public int levelHeight = 5;
    public Tilemap world;
    public Tilemap overlayed;
    public Tilemap hazards;
    public Tile[] grounds;
    public Tile[] obstacles;
    
    public GameObject zombiePrefab;


    // Start is called before the first frame update

    void Start() {
        Generate();
    }

    public void Generate(int width = -1, int height = -1) {
        if (width < 0)
            width = levelLength;
        if (height < 0)
            height = levelHeight;
        GameInfo.ZombieKilled(); //truss
        Vector3Int currentPos;
        int zombiesSpawned = 0;
        for (int i = 0; i < width; i++) {
            int lineObstacles = 0;
            for (int j = 0; j < height; j++) {
                currentPos = new Vector3Int(i, -j, j);
                world.SetTile(world.WorldToCell(currentPos), SelectGround());
                if (lineObstacles < 4 && Utils.RandomChance(5)) {
                    lineObstacles++;
                    // CHANGE THIS IF YOU CHANGE OBSTACLES
                    int randObstacle = Random.Range(0, obstacles.Length);
                    if (randObstacle > 2)
                        overlayed.SetTile(overlayed.WorldToCell(currentPos), obstacles[randObstacle]);
                    else
                        hazards.SetTile(overlayed.WorldToCell(currentPos), obstacles[randObstacle]);
                }
                else if (i > 20 && Utils.RandomChance(5)) {
                    Instantiate(zombiePrefab, world.WorldToCell(currentPos), Quaternion.identity);
                    zombiesSpawned++;
                }
            }
        }
        // safety check to make sure bounty can be completed
        for (int i = zombiesSpawned; i < GameInfo.goal; i++) {
            Vector3Int spawnPos = new Vector3Int(Random.Range(0, width), Random.Range(0, -height), 0);
            Instantiate(zombiePrefab, world.WorldToCell(spawnPos), Quaternion.identity);
        }
    }

    private Tile SelectGround() {
        return grounds[Random.Range(0, grounds.Length)];
    }

    private Tile SelectObstacle() {
        return obstacles[Random.Range(0, obstacles.Length)];
    }
}
