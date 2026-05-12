using UnityEngine;

public class Gun : MonoBehaviour
{
    public float impactForce = 500f;
    public float maxDistance = 100f;
    public Camera playerCamera;
    public GameObject impactEffect; // Assign a particle system prefab in the Inspector

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            Rigidbody rb = hit.collider.attachedRigidbody;
            if (rb != null)
            {
                rb.AddForceAtPosition(ray.direction * impactForce, hit.point);
            }

            if (impactEffect != null)
            {
                GameObject effect = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(effect, 2f); // Destroy after 2 seconds
            }
        }
    }
}