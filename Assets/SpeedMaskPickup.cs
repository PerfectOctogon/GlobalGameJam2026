using UnityEngine;

public class SpeedMaskPickup : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FirstPersonMovement movement =
                other.GetComponent<FirstPersonMovement>();

            if (movement != null)
            {
                movement.EnableSpeedPowerUp();
                Destroy(gameObject);
            }
        }
    }
}