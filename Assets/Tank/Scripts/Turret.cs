using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject rocket;
    [SerializeField] Transform nozzle;
    [SerializeField, Range(0.5f, 5)] float spawnTimeMin;
    [SerializeField, Range(0.5f, 5)] float spawnTimeMax;
    float spawnTime;

    float spawnTimer;
    void Start()
    {
        // spawnTimer = spawnTime;
        StartCoroutine(SpawnFire());
    }

    void Update()
    {
        // spawnTimer-= Time.deltaTime;
        // if(spawnTimer<=0)
        // {
        //     Instantiate(rocket, nozzle.position, nozzle.rotation);
        //     spawnTimer = spawnTime;
        // }
    }

    IEnumerator SpawnFire()
    {
        while(true)
        {
            spawnTime = UnityEngine.Random.Range(spawnTimeMin, spawnTimeMax);
            yield return new WaitForSeconds(spawnTime);
            Instantiate(rocket, nozzle.position, nozzle.rotation);
        }

    }
}
