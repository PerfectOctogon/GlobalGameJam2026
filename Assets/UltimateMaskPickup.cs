using UnityEngine;

public class UltimateMaskPickup : MonoBehaviour
{
    public AlarmScript alarmScript;
    public GameObject alarmSound;
    public GameObject blockedDoor;
    
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
                
                alarmScript.enabled = true;
                alarmSound.SetActive(true);
                blockedDoor.SetActive(true);

                Destroy(gameObject);
            }
        }
    }
}
