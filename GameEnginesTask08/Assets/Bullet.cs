using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit by projectile!");
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}