using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Coin Settings")]
    public string playerObjectName = "Player";
    public float attractDistance = 4f;
    public float flySpeed = 10f;
    public float collectDistance = 0.35f;

    private Transform player;
    private bool isFlyingToPlayer = false;

    void Start()
    {
        GameObject playerObj = GameObject.Find(playerObjectName);
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Coin: Player object not found. Set 'playerObjectName' correctly.");
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (!isFlyingToPlayer && distanceToPlayer <= attractDistance)
        {
            isFlyingToPlayer = true;
        }

        if (isFlyingToPlayer)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                flySpeed * Time.deltaTime
            );

            if (distanceToPlayer <= collectDistance)
            {
                if (CoinManager.Instance != null)
                {
                    CoinManager.Instance.AddCoin(1);
                }
                Destroy(gameObject);
            }
        }
    }
}
