using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HamsterSpawner : MonoBehaviour
{
    [SerializeField] Hamster hamsterPrefab;
    [SerializeField] Transform[] hamsterSpawnPoints;
    [SerializeField] float timeBetweenSpawns;
    float spawnTimer;
    [SerializeField] bool spawnFromStart;
    // Start is called before the first frame update
    void Start()
    {
        if(spawnFromStart){
            SpawnHamster();
        }
    }

    // Update is called once per frame
    void Update()
    {
        SpawnHamster();
    }

    private void SpawnHamster()
    {
        spawnTimer += Time.deltaTime;
        int spawnPointIndex = Random.Range(0, hamsterSpawnPoints.Length);
        if (spawnTimer > timeBetweenSpawns)
        {
            Hamster spawnedHamster = GameObject.Instantiate(hamsterPrefab.gameObject, hamsterSpawnPoints[spawnPointIndex].position, Quaternion.identity).GetComponent<Hamster>();
            spawnTimer = 0;
        }
    }
}
