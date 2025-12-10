using UnityEngine;

public class WeaponShotgun : WeaponBase
{
    public float damagePerPellet = 15f;
    public int pelletCount = 6;
    public float spread = 0.05f;

    public AudioSource shootSound;

    public override void Shoot(PlayerController player)
    {
        if (Time.time < nextFireTime) return;
        if (currentAmmo <= 0) return;

        nextFireTime = Time.time + fireRate;
        currentAmmo--;

        if (shootSound != null)
            shootSound.Play();

        player.UpdateUI();

        for (int i = 0; i < pelletCount; i++)
        {
            Vector3 dir = Camera.main.transform.forward;
            dir.x += Random.Range(-spread, spread);
            dir.y += Random.Range(-spread, spread);

            if (Physics.Raycast(Camera.main.transform.position, dir, out RaycastHit hit, range))
            {
                ZombieHealth zombie = hit.collider.GetComponent<ZombieHealth>();
                if (zombie != null)
                    zombie.TakeDamage(damagePerPellet);
            }
        }
    }
}
