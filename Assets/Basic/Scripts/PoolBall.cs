using System.Collections;
using UnityEngine;

public class PoolBall : MonoBehaviour, IPoolable<GameObject>
{
    public IPool<GameObject> Pool { get; set; }

    public void OnSpawn()
    {
        Debug.Log("Spawned");

        GetComponent<Rigidbody>()
        .AddForce(Random.insideUnitSphere * 50, ForceMode.VelocityChange);

        StartCoroutine(WaitToRelease(2));
    }

    public void OnDespawn()
    {
        Debug.Log("despawned");

        //Pool.Release(gameObject);

    }
    
    //bool proceed = false;

    IEnumerator WaitToRelease(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Pool.Release(gameObject);

        //yield return new WaitUntil(() => proceed);

        // while(true)
        // {
        //     //check perception code (so it doesn't use up too much processing on raycasts)
        //     yield return new WaitForSeconds(0.1f);
        // }
    }

    // void Start()
    // {

    // }

    // void Update()
    // {

    // }
}
