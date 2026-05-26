using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    public string playerObjectName = "Player"; // Set this to your player's object name
    public float followDistance = 3f; // Distance behind the player
    public float leftRightOffset = 0f; // Left/right offset relative to the player
    public float height = 5f; // Base height above ground
    public float sineAmplitude = 1f; // Amplitude of sine wave
    public float sineFrequency = 1f; // Frequency of sine wave
    public float followSpeed = 5f; // How fast the drone follows the player

    [Header("Shooting Settings")]
    public GameObject bulletPrefab; // Assign your Bullet prefab in the Inspector
    public float shootInterval = 1.5f; // Interval between shots (seconds)
    public float bulletSpeed = 20f; // Speed of the bullet
    private float lastShootTime = -Mathf.Infinity;

    private Transform player;
    private Vector3 offset;
    private float startY;

    void Start()
    {
        GameObject playerObj = GameObject.Find(playerObjectName);
        if (playerObj != null)
        {
            player = playerObj.transform;
            // Offset so drone stays behind and above the player
            offset = new Vector3(0, height, -followDistance);
            startY = transform.position.y;
        }
        else
        {
            Debug.LogWarning("DroneController: Player object not found. Set 'playerObjectName' to your player's name.");
        }
    }

    void Update()
    {
        if (player == null) return;

        // Target position: follow player's X/Z, offset behind, left/right, and add sine wave to Y
        Vector3 rightOffset = player.right * leftRightOffset;
        Vector3 targetPos = player.position + player.forward * -followDistance + rightOffset;
        targetPos.y = player.position.y + height + Mathf.Sin(Time.time * sineFrequency) * sineAmplitude;

        // Smooth follow
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);

        // Shooting logic
        TryShootAtZombie();
    }

    void TryShootAtZombie()
    {
        if (bulletPrefab == null) return;
        if (Time.time - lastShootTime < shootInterval) return;

        // Find all zombies in the scene
        ZombieController[] zombies = FindObjectsOfType<ZombieController>();
        if (zombies.Length == 0) return;

        // Find the closest zombie
        Transform closestZombie = null;
        float minDist = Mathf.Infinity;
        foreach (var zombie in zombies)
        {
            float dist = Vector3.Distance(transform.position, zombie.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closestZombie = zombie.transform;
            }
        }

        if (closestZombie != null)
        {
            ShootAt(closestZombie);
            lastShootTime = Time.time;
        }
    }

    void ShootAt(Transform target)
    {
        // Instantiate bullet at drone's position, facing the target
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector3 dir = (target.position + Vector3.up * 1f - transform.position).normalized;
        // If the bullet has a Rigidbody, set its velocity
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = dir * bulletSpeed;
        }
        // Optionally, rotate bullet to face direction
        bullet.transform.forward = dir;
    }
}
