using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset;   // Offset between the camera and the player

    // Shake Settings
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.2f;
    
    void LateUpdate()
    {
        if (player == null) return;

        // CAlculate standart follow position
        Vector3 targetPos = new Vector3(0f, player.position.y + offset.y, player.position.z + offset.z);

        // Add Shake
        if (shakeDuration > 0)
        {
            // Add random offset to X and Y
            targetPos += Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime;
        }

        transform.position = targetPos;
    }

    // Call this when player dies
    public void TriggerShake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}
