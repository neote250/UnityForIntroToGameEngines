using Unity.VisualScripting;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    [SerializeField] int AmmoCount = 5;
    [SerializeField] GameObject pickupFX;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(other.TryGetComponent(out PlayerTank component))
            {
                component.ammo += AmmoCount;
                Destroy(gameObject);
                if(pickupFX != null)
                {
                    Instantiate(pickupFX, transform.position, Quaternion.identity);
                }
            }

        }
    }
}
