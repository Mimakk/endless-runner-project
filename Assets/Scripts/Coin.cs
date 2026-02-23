using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed = 100f;
    void Start()
    {
       
    }

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Notify GameManager to add coin
            GameManager.Instance.AddCoin();

            // Destroy the coin
            Destroy(gameObject);
        }
    }
}
