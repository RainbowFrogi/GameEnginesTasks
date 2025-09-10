using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject bulletPrefab;  // The bullet to fire
    public Transform firePos;        // Position where bullets spawn
    public Transform playerPos;      // Reference to player's position
    public float fireInterval = 3f; // Time between shots

    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float baseArcHeight = 2f; // Minimum arc height
    [SerializeField] private float arcHeightMultiplier = 0.3f; // How much height increases per unit distance

    private void Update()
    {
        fireInterval -= Time.deltaTime;
       
        if (fireInterval <= 0f)
        {
            FireAtPlayer();
            fireInterval = 3f; // Reset interval
        }
    }
    
    private void FireAtPlayer()
    {    
        // Calculate dynamic arc height based on distance
        float distance = Vector3.Distance(firePos.position, playerPos.position);
        float dynamicArcHeight = baseArcHeight + (distance * arcHeightMultiplier);
        
        // Calculate the parabolic trajectory
        Vector3 velocity = CalculateParabolicVelocity(firePos.position, playerPos.position, dynamicArcHeight);
        
        // Spawn bullet
        GameObject bullet = Instantiate(bulletPrefab, firePos.position, Quaternion.LookRotation(velocity));
        
        // Apply calculated velocity
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = velocity;
        }
    }

    private Vector3 CalculateParabolicVelocity(Vector3 startPos, Vector3 targetPos, float height)
    {
        // Get the distance and displacement
        Vector3 displacement = targetPos - startPos;
        Vector3 horizontalDisplacement = new Vector3(displacement.x, 0, displacement.z);
        float horizontalDistance = horizontalDisplacement.magnitude;
        float verticalDisplacement = displacement.y;

        // Calculate time of flight using the arc height
        float gravity = Mathf.Abs(Physics.gravity.y);
        float timeToReachHeight = Mathf.Sqrt(2f * height / gravity);
        float timeToFall = Mathf.Sqrt(2f * (height + verticalDisplacement) / gravity);
        float totalTime = timeToReachHeight + timeToFall;

        // Calculate velocity components
        Vector3 horizontalVelocity = horizontalDisplacement / totalTime;
        float verticalVelocity = Mathf.Sqrt(2f * gravity * height);

        return horizontalVelocity + Vector3.up * verticalVelocity;
    }
}
