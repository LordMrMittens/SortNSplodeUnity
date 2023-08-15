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
    public bool ShouldBeFrozen {get;set;} = false;
    // Start is called before the first frame update
    void Start()
    {
        if(spawnFromStart){
            SpawnHamster();
        }
        GameManager.OnBoxIsMoving += SetFrozen;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ShouldBeFrozen)
        {
            SpawnHamster();
        }
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
        void SetFrozen(bool _isFrozen)
    {

    }
}
