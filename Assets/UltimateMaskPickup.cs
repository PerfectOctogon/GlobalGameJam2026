using UnityEngine;

public class UltimateMaskPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FirstPersonMovement movement =
                other.GetComponent<FirstPersonMovement>();

            if (movement != null)
            {
                movement.EnableDashPowerUp();
                movement.EnableDoubleJumpPowerUp();
                movement.EnableSpeedPowerUp();

                Destroy(gameObject);
            }
        }
    }
}
