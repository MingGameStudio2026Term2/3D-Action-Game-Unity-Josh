using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    [Header("UI")]
    public TextMeshProUGUI coinText;

    private int coinCount = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        UpdateCoinUI();
    }

    public void AddCoin(int amount = 1)
    {
        coinCount += amount;
        UpdateCoinUI();
    }

    public bool TrySpendCoins(int amount)
    {
        if (amount <= 0)
        {
            return true;
        }

        if (coinCount < amount)
        {
            return false;
        }

        coinCount -= amount;
        UpdateCoinUI();
        return true;
    }

    public int GetCoinCount()
    {
        return coinCount;
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = $"Coins: {coinCount}";
        }
    }
}
