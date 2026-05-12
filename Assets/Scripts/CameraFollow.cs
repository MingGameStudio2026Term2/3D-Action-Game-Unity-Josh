using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float distance = 8f;

    public float pitchSpeed = 2f;

    public float minPitch = 10f;
    public float maxPitch = 70f;

    public float lookHeightOffset = 5f;

    private float currentPitch = 35f;

    void LateUpdate()
    {
        float mouseY = Input.GetAxis("Mouse Y");
        currentPitch -= mouseY * pitchSpeed;
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);


        float horizontalDist = distance * Mathf.Cos(currentPitch * Mathf.Deg2Rad);
        float verticalDist = distance * Mathf.Sin(currentPitch * Mathf.Deg2Rad);


        Vector3 targetPosition = target.position
            - target.forward * horizontalDist
            + Vector3.up * verticalDist;

        transform.position = targetPosition;
        transform.LookAt(target.position + Vector3.up * lookHeightOffset);
    }
}