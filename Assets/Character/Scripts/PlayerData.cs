using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Range(0,10)] public float speed = 3;
    [Range(0,10)] public float sprintSpeed = 3;
    [Range(0,10)] public float acceleration = 3;


    [Range(0,10)] public float jumpHeight = 2;
    [Range(0,-20)] public float gravity = -9.8f;
    [Range(0,20)] public float turnRate = 1;

    [Range(0,10)] public float pushForce = 5;
}
