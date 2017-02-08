using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour {
    public static EnemySpawner Instance;
    public float timeToFirstWave = 1;
    public float spawnStartRate = 5;
    public float spawnIncreaseRate = 1.1f;
    public Enemy[] enemyPrefabs;
    private float timer = 0;
    private float spawnRate;
    
    private bool[] busyEnemies;

    private void Awake() {
        Instance = this;
        spawnRate = spawnStartRate;
        busyEnemies = new bool[enemyPrefabs.Length];
    }

    private void Update() {
        timer += Time.deltaTime;
        if(timer > spawnRate) {
            Spawn();
            timer = 0;
            spawnRate *= (1 / spawnIncreaseRate);
        }
    }

    void Spawn() {
        var enemyIndex = Random.Range(0, enemyPrefabs.Length);
        if(!busyEnemies[enemyIndex]) {
            busyEnemies[enemyIndex] = true;
            var enemyInstance = Instantiate(enemyPrefabs[enemyIndex]);
            enemyInstance.transform.position = enemyPrefabs[enemyIndex].transform.position;
            enemyInstance.gameObject.SetActive(true);
            enemyInstance.enemyID = enemyIndex;
        }
    }

    public void OnEnemyDie(Enemy enemy) {
        busyEnemies[enemy.enemyID] = false;
    }
}
