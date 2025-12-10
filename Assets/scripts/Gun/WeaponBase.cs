using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public string weaponName;

    public int maxAmmo;        // sức chứa băng đạn
    public int currentAmmo;    // đạn trong băng
    public float fireRate = 0.2f;
    public float range = 150f;

    public AmmoType ammoType;

    public int reserveAmmo = 90;     // 🔥 đạn dự trữ
    public int maxReserveAmmo = 90;  // 🔥 tối đa mang theo

    protected float nextFireTime = 0f;

    public abstract void Shoot(PlayerController player);

    public virtual void Reload()
    {
        int needed = maxAmmo - currentAmmo;       // cần bao nhiêu viên để đầy băng?

        if (reserveAmmo <= 0 || needed <= 0)
            return;

        int loadAmount = Mathf.Min(needed, reserveAmmo);
        currentAmmo += loadAmount;
        reserveAmmo -= loadAmount;
    }
}
