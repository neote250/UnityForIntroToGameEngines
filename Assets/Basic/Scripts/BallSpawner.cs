using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private PoolSO ballPool; //Pool or PoolSO
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GameObject ball = ballPool.Get();
            ball.transform.position = transform.position;
            ball.SetActive(true);
        }
    }
}
