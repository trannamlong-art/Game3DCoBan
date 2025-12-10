using UnityEngine;
using System.Collections;

public class WeaponSpawner : MonoBehaviour
{
    [Header("Weapon List")]
    public GameObject[] weaponPrefabs;   // danh sách súng có thể spawn

    [Header("Spawn Area")]
    public Vector3 minSpawnPos;          // góc dưới map
    public Vector3 maxSpawnPos;          // góc trên map

    [Header("Spawn Time")]
    public float minSpawnTime = 10f;     // thời gian spawn ngẫu nhiên nhỏ nhất
    public float maxSpawnTime = 25f;     // thời gian spawn ngẫu nhiên lớn nhất

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            SpawnWeapon();
        }
    }

    void SpawnWeapon()
    {
        if (weaponPrefabs.Length == 0) return;

        // ✅ chọn ngẫu nhiên 1 khẩu súng
        int randomIndex = Random.Range(0, weaponPrefabs.Length);
        GameObject weaponToSpawn = weaponPrefabs[randomIndex];

        // ✅ vị trí spawn ngẫu nhiên trong map
        Vector3 randomPos = new Vector3(
            Random.Range(minSpawnPos.x, maxSpawnPos.x),
            minSpawnPos.y,
            Random.Range(minSpawnPos.z, maxSpawnPos.z)
        );

        Instantiate(weaponToSpawn, randomPos, Quaternion.identity);
    }
}
