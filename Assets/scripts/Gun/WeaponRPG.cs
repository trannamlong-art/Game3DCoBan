using UnityEngine;

public class WeaponRPG : WeaponBase
{
    public GameObject rocketPrefab;
    public Transform shootPoint;
    public float rocketSpeed = 50f;

    public override void Shoot(PlayerController player)
    {
        if (Time.time < nextFireTime) return;
        if (currentAmmo <= 0) return;

        nextFireTime = Time.time + fireRate;
        currentAmmo--;

        player.UpdateUI();

        GameObject rocket = Instantiate(
            rocketPrefab,
            shootPoint.position,
            shootPoint.rotation
        );

        Rigidbody rb = rocket.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = shootPoint.forward * rocketSpeed;
    }
}
