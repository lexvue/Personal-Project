using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject [] enemyPrefabs;

    private Vector3 spawnPos = new Vector3(25, 0, 0);
    public GameObject rotation;
    private float spawnPosX = 25;
    private float spawnLimitHiY = 6;
    private float spawnLimitLoY = 3;

    private float spawnInterval;
    private float spawnRandLo = 3;
    private float spawnRandHi = 6;

    private const float loRange = 3;
    private const float hiRange = 6;

    private PlayerController _playerController;

    void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public IEnumerator SpawnInterval(float difficulty) {
        spawnRandLo = loRange / difficulty;
        spawnRandHi = hiRange / difficulty;
        spawnInterval = Random.Range(spawnRandLo, spawnRandHi);
        Debug.Log("Spawn Interval = " + spawnInterval);
        yield return new WaitForSeconds(spawnInterval);
        SpawnRandomEnemy();
        StartCoroutine(SpawnInterval(difficulty));
    }

    void SpawnRandomEnemy() {
        if (!_playerController.gameOver) {
            GameObject enemy;
            int num = Random.Range(0,3);
            if (num == 0) {
                Vector3 spawnFlyPos = new Vector3(spawnPosX, Random.Range(spawnLimitLoY, spawnLimitHiY), 0);
                enemy = enemyPrefabs[num];
                Instantiate(enemy, spawnFlyPos, rotation.transform.rotation);
            } else {
                enemy = enemyPrefabs[num];
                Instantiate(enemy, spawnPos, rotation.transform.rotation);
            }
        }
    }
}
