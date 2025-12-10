using UnityEngine;
using System.Collections;

public class WeaponSpawnPoint : MonoBehaviour
{
    [Header("Weapon List")]
    public GameObject[] weaponPrefabs;

    [Header("Spawn Time")]
    public float minSpawnTime = 10f;
    public float maxSpawnTime = 25f;

    [Header("Beam Effect")]
    public GameObject beamPrefab;   // ✅ prefab cột sáng
    private GameObject currentBeam; // cột sáng đang hiển thị

    private GameObject currentWeapon;
    private bool isSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            if (currentWeapon == null && !isSpawning)
            {
                isSpawning = true;

                float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
                yield return new WaitForSeconds(waitTime);

                SpawnWeapon();
                isSpawning = false;
            }

            yield return null;
        }
    }

    void SpawnWeapon()
    {
        if (weaponPrefabs.Length == 0) return;

        int randomIndex = Random.Range(0, weaponPrefabs.Length);
        GameObject weaponPrefab = weaponPrefabs[randomIndex];

        // ✅ SPAWN VŨ KHÍ
        currentWeapon = Instantiate(
            weaponPrefab,
            transform.position,
            transform.rotation,
            transform   // cho làm con của spawn point
        );

        // ✅ SPAWN CỘT SÁNG
        if (beamPrefab != null)
        {
            currentBeam = Instantiate(
                beamPrefab,
                transform.position,
                Quaternion.identity,
                transform   // cho làm con của spawn point
            );
        }
    }

    // ✅ khi vũ khí bị nhặt
    public void ClearWeapon()
    {
        if (currentBeam != null)
            Destroy(currentBeam);  // ✅ TẮT CỘT SÁNG

        currentWeapon = null;
    }
}
