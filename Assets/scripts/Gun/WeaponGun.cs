using UnityEngine;

public class WeaponGun : WeaponBase
{
    public float damage = 30f;
    public AudioSource shootSound;

    public override void Shoot(PlayerController player)
    {
        if (Time.time < nextFireTime) return;
        if (currentAmmo <= 0) return;

        nextFireTime = Time.time + fireRate;
        currentAmmo--;
        player.UpdateUI();

        if (shootSound != null)
            shootSound.Play();

        // Raycast bắn vào giữa màn hình
        Ray ray = Camera.main.ScreenPointToRay(
            new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            ZombieHealth zombie = hit.collider.GetComponent<ZombieHealth>();
            if (zombie != null)
                zombie.TakeDamage(damage);
        }
    }
}
