using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public float waveSize = 3;
    public float timeToFirstWave = 1;
    public float waveDuration = 5;
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;
    private List<Transform> spawnPointsList = new List<Transform>();

    private List<GameObject> enemiesInstances = new List<GameObject>();

    private void Start() {
        InvokeRepeating("SpawnWave", timeToFirstWave, waveDuration);
    }

    void SpawnWave() {
        spawnPointsList = new List<Transform>(spawnPoints);
        CleanWave();
        var waveRealSize = Random.Range(1, waveSize + 1);
        for(int i = 0; i < waveRealSize; i++) {
            Spawn();
        }
    }


    void CleanWave() {
        foreach(GameObject g in enemiesInstances) {
            if(g != null) {
                Debug.Log("there is some enemy left...");
                GameController.Instance.GameOver();
            }
            Destroy(g);
        }
            

        enemiesInstances.Clear();
    }

    void Spawn() {
        var enemyInstance = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]);
        var spawnPoint = spawnPointsList[Random.Range(0, spawnPointsList.Count)];
        enemyInstance.transform.position = spawnPoint.position;
        spawnPointsList.Remove(spawnPoint);
        enemyInstance.SetActive(true);
        enemiesInstances.Add(enemyInstance);
    }
}
