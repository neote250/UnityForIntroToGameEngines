using Unity.Mathematics;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Range(1,10)]public float speed = 2;
    public GameObject prefab;
    void Awake()
    {
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {    
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        Vector3 velocity = Vector3.zero;
        
        velocity.x = Input.GetAxis("Horizontal");
        velocity.z = Input.GetAxis("Vertical");
        transform.position += velocity * speed * Time.deltaTime;

        //create prefab
        if(Input.GetKey(KeyCode.Space))
        {
            Instantiate(prefab, this.transform.position + Vector3.up, quaternion.identity);
        }
        //if(Input.GetButton("Fire1"))
        //{
        //    position.y += 1 * Time.deltaTime;
        //}
    }
}
