using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;
    private void Awake()
    {
        Instance = this;
    }

    [Serializable]
    public class EnemyWaves
    {
        public string WaveName;
        [Space]
        public float WaveLength;
        [Space]
        public GameObject[] Enemys;
        public int[] SpawnChances;
        [Space]
        public bool KeepSpawning;
        public float MinSpawnrate;
        public float MaxSpawnrate;
    }

    public EnemyWaves[] Waves;

    int currentWave;

    bool spawn;
    float spawnrate;
    float timer;

    [SerializeField] float TimeBeforeSpawn = 0.2f;
    [Space]
    [SerializeField] Transform[] Spawnpoints;
    float timeEachSpawn;

    private void Start()
    {
        StartWaves();
    }

    void StartWaves()
    {
        currentWave = 0;

        if (Waves[currentWave].KeepSpawning == true)
        {
            timer = 0;
            spawnrate = UnityEngine.Random.Range(Waves[currentWave].MinSpawnrate, Waves[currentWave].MaxSpawnrate);
            spawn = true;
        }
        else
        {
            SpawnEnemy();
            spawn = false;
        }

        Invoke(nameof(EndWave), Waves[currentWave].WaveLength);
    }

    private void Update()
    {
        if (!spawn)
            return;

        if (timer < spawnrate)
            timer += Time.deltaTime;
        else
        {
            timer = 0;
            spawnrate = UnityEngine.Random.Range(Waves[currentWave].MinSpawnrate, Waves[currentWave].MaxSpawnrate);

            SpawnEnemy();
        }
    }

    public void EndWave()
    {
        if (currentWave < Waves.Length - 1)
            currentWave++;
        else
            return;

        if (Waves[currentWave].KeepSpawning == true)
        {
            timer = 0;
            spawnrate = UnityEngine.Random.Range(Waves[currentWave].MinSpawnrate, Waves[currentWave].MaxSpawnrate);
            spawn = true;
        }
        else
        {
            SpawnEnemy();
            spawn = false;
        }

        Invoke(nameof(EndWave), Waves[currentWave].WaveLength);
    }

    void SpawnEnemy()
    {
        int spawnNum = Waves[currentWave].SpawnChances[UnityEngine.Random.Range(0, Waves[currentWave].SpawnChances.Length)];
        for (int i = 0; i < spawnNum; i++)
        {
            Invoke(nameof(InstanceEnemy), timeEachSpawn);
            timeEachSpawn += TimeBeforeSpawn;
            if (i >= spawnNum) timeEachSpawn = 0;
        }
    }

    void InstanceEnemy()
    {
        Instantiate(Waves[currentWave].Enemys[UnityEngine.Random.Range(0, Waves[currentWave].Enemys.Length)], Spawnpoints[UnityEngine.Random.Range(0, Spawnpoints.Length)].position, Quaternion.identity);
    }
}
