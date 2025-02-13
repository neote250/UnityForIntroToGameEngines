using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBirdController : MonoBehaviour
{
    CharacterController characterController;
    Vector3 velocity = Vector3.right;
    [SerializeField] float timer = 0.3f;
    [SerializeField] float gravity = 10;
    [SerializeField] float jumpStrength = 10;

    bool cooldown = false;

    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        velocity.y += -gravity;

        if (Input.GetKey(KeyCode.Space) && cooldown == false)
        {
            cooldown = true;
            velocity.y = jumpStrength;
            StartCoroutine(CooldownTimer());
        }
        characterController.Move(velocity * Time.deltaTime);
    }

    private IEnumerator CooldownTimer()
    {
        yield return new WaitForSeconds(timer);
        cooldown = false;
    }
}
