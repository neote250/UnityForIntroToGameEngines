using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTank : MonoBehaviour
{
    [SerializeField] float maxTorque = 90;
    [SerializeField] float maxForce = 1;
    [SerializeField] Transform nozzle;
    [SerializeField] GameObject rocket;
    [SerializeField] TMP_Text ammoText;
    [SerializeField] Slider healthSlider;
    public int ammo = 10;

    float torque;
    float force;

    Rigidbody rb;
    Destructable destructible;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        destructible = GetComponent<Destructable>();
    }

    void Update()
    {
        torque = Input.GetAxis("Horizontal") * maxTorque;
        force = Input.GetAxis("Vertical") * maxForce;

        if(Input.GetButtonDown("Fire1") && ammo > 0)
        {
            ammo--;
            Instantiate(rocket, nozzle.position, nozzle.rotation);
        }

        ammoText.text = "Ammo: " + ammo.ToString();

        healthSlider.value = destructible.Health;
        if(destructible.Health <=0)
        {
            GameManager.Instance.SetGameOver();
        }
    }

    private void FixedUpdate()
    {
        rb.AddRelativeForce(Vector3.forward * force);
        rb.AddRelativeTorque(Vector3.up * torque/*, ForceMode.Impulse*/);
    }
}
