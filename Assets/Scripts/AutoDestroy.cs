using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    private Transform player;
    private float destroyDistance = 10f; // How far behind before destroy
    void Start()
    {
        // Finds player slightly inefficiently, but ok for spawning once evey few second
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (transform.position.z < player.position.z - destroyDistance)
        {
            // Dont destroy. Just turn off.
            gameObject.SetActive(false);
        }
    }
}
