using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    public string playerObjectName = "Player"; // Set this to your player's object name
    public float followDistance = 3f; // Distance behind the player
    public float height = 5f; // Base height above ground
    public float sineAmplitude = 1f; // Amplitude of sine wave
    public float sineFrequency = 1f; // Frequency of sine wave
    public float followSpeed = 5f; // How fast the drone follows the player

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

        // Target position: follow player's X/Z, offset behind, and add sine wave to Y
        Vector3 targetPos = player.position + player.forward * -followDistance;
        targetPos.y = player.position.y + height + Mathf.Sin(Time.time * sineFrequency) * sineAmplitude;

        // Smooth follow
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
    }
}
