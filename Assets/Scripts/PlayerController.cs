using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Speed Settings")]
    public float forwardSpeed = 10f;
    public float maxSpeed = 25f; // Speed cap
    public float acceleration = 0.1f; // How much speed to add per second

    [Header("Lane Settings")]
    public float laneSpeed = 15f; // Speed of moving left/right
    public float laneOffset = 1.5f; // Distance from center

    [Header("Camera")]
    public CameraFollow cameraScript;

    private int currentLane = 1; // 0: left, 1: right
    private float targetX; // Where we want to be
    private bool isDead = false;


    void Start()
    {
        // Start in the right lane
        currentLane = 1;
        targetX = laneOffset;
    }

    void Update()
    {
        if (!GameManager.Instance.isGameStarted) return; // Don't move if game not started
        if (isDead) return;

        // Every second, we add a tiny bit of speed until reach max
        if (forwardSpeed < maxSpeed)
        {
            forwardSpeed += acceleration * Time.deltaTime;
        }

        // Move forward
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // When you Tap / Click to switch lanes
        if (Input.GetMouseButtonDown(0))
        {
            SwitchLane();
        }

        // Smoothly move to the target lane position
        Vector3 newPosition = transform.position;
        // Lerp moves value a to b over time t
        newPosition.x = Mathf.Lerp(transform.position.x, targetX, laneSpeed * Time.deltaTime);
        transform.position = newPosition;

    }

    private void SwitchLane()
    {
        currentLane = 1 - currentLane; // Toggle between 0 and 1
        targetX = (currentLane == 0) ? -laneOffset : laneOffset; // another way to write if currentLane == 0 than targetX -laneOffset else laneOffset
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return; // Prevents double deaths

        if (other.CompareTag("Obstacle"))
        {
            isDead = true;

            // Trigger Shake (Duration 0.5s, Stregth 0.5)
            if (cameraScript != null) cameraScript.TriggerShake(0.5f, 0.5f);

            // Hides player
            GetComponentInChildren<Renderer>().enabled = false;

            // delays Game Over screen for 1 second
            Invoke("CallGameOver", 1f);
        }
        
    }

    void CallGameOver()
    {
        GameManager.Instance.EndGame();
    }
}
