using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoadBlock : MonoBehaviour
{
    private bool playerInRange = false;
    private bool isBuilt = false;
    public int buildCost = 5;
    public GameObject brokenRoadblockChild;
    public GameObject finishedRoadblock;

    void Start()
    {
        if (brokenRoadblockChild != null)
        {
            brokenRoadblockChild.SetActive(true);
        }

        if (finishedRoadblock != null)
        {
            finishedRoadblock.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Press E to build the road block");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        if (isBuilt) return;

        if (playerInRange && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (CoinManager.Instance != null && CoinManager.Instance.TrySpendCoins(buildCost))
            {
                if (brokenRoadblockChild != null)
                {
                    brokenRoadblockChild.SetActive(false);
                }

                if (finishedRoadblock != null)
                {
                    finishedRoadblock.SetActive(true);
                }

                isBuilt = true;
                Debug.Log($"Road block built. Spent {buildCost} coins.");
                playerInRange = false;
            }
            else
            {
                int currentCoins = CoinManager.Instance != null ? CoinManager.Instance.GetCoinCount() : 0;
                Debug.Log($"Not enough coins to build road block. Need {buildCost}, have {currentCoins}.");
            }
        }
    }
}
