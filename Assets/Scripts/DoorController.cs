using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public string playerTag = "Player";
    public float openAngle = 90f;
    public float openSpeed = 2f;

    private Transform doorTransform;
    private bool isOpening = false;
    private bool hasOpened = false;
    private Quaternion targetRotation;

    void Start()
    {
        doorTransform = transform.Find("Door");
        if (doorTransform == null)
        {
            Debug.LogError("No child named 'Door' found!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasOpened && other.CompareTag(playerTag) && doorTransform != null)
        {
            targetRotation = Quaternion.Euler(0, openAngle, 0) * doorTransform.localRotation;
            isOpening = true;
            hasOpened = true;
        }
    }

    void Update()
    {
        if (isOpening && doorTransform != null)
        {
            doorTransform.localRotation = Quaternion.Lerp(doorTransform.localRotation, targetRotation, Time.deltaTime * openSpeed);
            if (Quaternion.Angle(doorTransform.localRotation, targetRotation) < 1f)
            {
                doorTransform.localRotation = targetRotation;
                isOpening = false;
            }
        }
    }
}
