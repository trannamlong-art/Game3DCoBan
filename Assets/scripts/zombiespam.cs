using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [Header("Zombie")]
    public GameObject zombiePrefab;

    [Header("Spawn Settings")]
    public Transform[] spawnPoints;   // ✅ Các điểm spawn đặt sẵn
    public float spawnDelay = 3f;     // ✅ Thời gian giữa mỗi lần spawn
    public int maxZombies = 20;       // ✅ Số zombie tối đa trong map

    private int currentZombieCount = 0;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);

            if (currentZombieCount >= maxZombies)
                continue;

            SpawnZombie();
        }
    }

    void SpawnZombie()
    {
        if (spawnPoints.Length == 0 || zombiePrefab == null) return;

        Transform randomPoint =
            spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(zombiePrefab, randomPoint.position, randomPoint.rotation);

        currentZombieCount++;
    }

    // ✅ GỌI HÀM NÀY KHI ZOMBIE CHẾT
    public void ZombieDied()
    {
        currentZombieCount--;
        if (currentZombieCount < 0)
            currentZombieCount = 0;
    }
}
