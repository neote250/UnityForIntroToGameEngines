using UnityEngine;

public class HealthPickUp : MonoBehaviour
{

    [SerializeField] int healthBonus = 2;
    [SerializeField] GameObject pickupFX;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(other.TryGetComponent(out PlayerTank component))
            {
                //component.GetComponent<Destructable>().Health += healthBonus;
                Destroy(gameObject);
                if(pickupFX != null)
                {
                    Instantiate(pickupFX, transform.position, Quaternion.identity);
                }
            }

        }
    }
}

