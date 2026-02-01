using UnityEngine;

public class DoubleJumpMaskPickup : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FirstPersonMovement movement =
                other.GetComponent<FirstPersonMovement>();

            if (movement != null)
            {
                movement.EnableDoubleJumpPowerUp();
                Destroy(gameObject);
            }
        }
    }
}