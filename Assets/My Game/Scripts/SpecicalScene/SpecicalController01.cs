using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class SpecicalController01 : MonoBehaviour
{
    public static SpecicalController01 Instance;

    [Header("Spike")]
    public GameObject spikePrefab01;
    public GameObject spikePrefab02;
    public float speedSpike = 5f;
    public float yMax = -1f;
    public float yMin = -3f;
    public float xPosSpawn = 10f;

    public float spawnTime = 2f;
    float m_spawnTimeSpike;

    bool isGameover = false;

    [Header("Coin")]
    public GameObject coinPrefab;
    public float yMaxCoin = 1.15f;
    public float yMinCoin = -1.15f;
    public float xMaxPosSpawnCoin = 12f;
    public float xMinPosSpawnCoin = 9f;
    public float spawnTimeCoin = 1f;
    float m_spawnTimeCoin;

    [Header("Background")]
    public GameObject windowPrefab;
    float m_spawnTimeWindow;

  //  private int totalCoin = 0;
    public int coinForIncreaseSpeed = 0;
    bool isSaveCoin;
    public bool IsGameover { get => isGameover; set => isGameover = value; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    private void Update()
    {
        if (isGameover) {
            return;
        }
        m_spawnTimeSpike -= Time.deltaTime;
        m_spawnTimeCoin -= Time.deltaTime;
        if (m_spawnTimeSpike <= 0)
        {
            SpawnSpike();
            SpawnWindow();
            m_spawnTimeSpike = spawnTime;
        }
        if (m_spawnTimeCoin <= 0) {
            SpawnCoin();
            m_spawnTimeCoin = spawnTimeCoin;
        }
    }
    public void SpawnSpike()
    {
        if (spikePrefab01 == null || spikePrefab02 == null) return;
        GameObject spikePrefab = Random.Range(0f, 1f) < 0.6f ? spikePrefab01 : spikePrefab02;
        float yPos = Random.Range(yMin, yMax);
        Vector2 spawnPos = new Vector2(xPosSpawn, yPos);
        GameObject spikeClone = Instantiate(spikePrefab, spawnPos, Quaternion.identity);
        spikeClone.GetComponent<MovingSpecical01>().moveSpeed = speedSpike;
    }
    public void IncreaseCoin(int coin)
    { 
        if(GameManager.Instance == null) return;
        GameManager.Instance.TotalCoins += coin;
    //    totalCoin += coin;
        GUIManager.Instance?.UpdateTextCoin(GameManager.Instance.TotalCoins);
        coinForIncreaseSpeed += coin;
        if (coinForIncreaseSpeed >= 500)
        {
            speedSpike++;
            coinForIncreaseSpeed = 0;
        }

    }
    public void SpawnWindow()
    {
        if (windowPrefab == null) return;
        float xPos = Random.Range(10f, 15f);
        Vector2 spawnPos = new Vector2(xPos, 1.17f);
        GameObject windowClone = Instantiate(windowPrefab, spawnPos, Quaternion.identity);
        windowClone.GetComponent<MovingSpecical01>().moveSpeed = speedSpike;
    }
    public void SpawnCoin()
    {
        if (coinPrefab == null) return;
        float yPos = Random.Range(yMinCoin, yMaxCoin);
        float xPos = Random.Range(xMinPosSpawnCoin, xMaxPosSpawnCoin);
        Vector2 spawnPos = new Vector2(xPos, yPos);
        GameObject coinClone = Instantiate(coinPrefab, spawnPos, Quaternion.identity);
        coinClone.GetComponent<CoinSpecical01>().moveSpeed = speedSpike;
    }
    public void Complete()
    {
        if (IsGameover && !isSaveCoin)
        {
            GameManager.Instance.CalculateStarsForLevel();
          //  Pref.Coins += totalCoin;
            Time.timeScale = 0f;
            if (Pref.IsSpecialLevel(Pref.CurrentLevelPlay))
            {
                Pref.CompleteSpecialLevel(Pref.CurrentLevelPlay);
            }
            GameManager.Instance.WinLevel();
            isSaveCoin = true   ;
        }
    }
}
