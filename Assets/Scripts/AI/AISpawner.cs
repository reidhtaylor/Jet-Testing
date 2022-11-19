using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
    public AI aiPrefab;

    [Header("Enemy")]
    public int initialEnemies = 15;
    public int enemySpawnRate = 5;
    public int maxEnemies = 30;

    [Header("Friend")]
    public int initialFriends = 5;
    public int friendSpawnRate = 5;
    public int maxFriends = 10;

    private float lastEnemySpawn;
    private float lastFriendSpawn;
    private List<AI> enemies = new List<AI>();
    private List<AI> friends = new List<AI>();

    private void Start() {
        Spawn(initialEnemies, true);
        Spawn(initialFriends, false);
    }

    private void Update() {        
        if (enemies.Count <= maxEnemies && enemySpawnRate != 0 && Time.time >= lastEnemySpawn + (60 / enemySpawnRate)) {
            Spawn(1, true);
            lastEnemySpawn = Time.time;
        }
        if (friends.Count <= maxFriends && friendSpawnRate != 0 && Time.time >= lastFriendSpawn + (60 / friendSpawnRate)) {
            Spawn(1, false);
            lastFriendSpawn = Time.time;
        }
    }

    public void Spawn(int c = 1, bool enemy =  true) {
        AI ai;
        for (int i = 0; i < c; i++) {
            ai = Instantiate(aiPrefab, MapBounds.RandomPoint(), Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up), transform).GetComponent<AI>();
            ai.OnInitialize(!enemy);
            if (enemy) enemies.Add(ai);
            else friends.Add(ai);
        }
    }
}
