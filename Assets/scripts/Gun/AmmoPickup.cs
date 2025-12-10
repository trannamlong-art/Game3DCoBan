using UnityEngine;
using System.Collections;

public class AmmoPickup : MonoBehaviour
{
    public AmmoType ammoType;
    public int ammoAmount = 30;
    public float respawnTime = 10f;

    private bool canPickup = false;
    private MeshRenderer mesh;
    private Collider col;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();

        UpdateActiveState(); // ✅ kiểm tra lúc bắt đầu

        if (WeaponUnlockManager.Instance != null)
        {
            WeaponUnlockManager.Instance.OnAmmoUnlocked += HandleAmmoUnlocked;
        }
    }

    void HandleAmmoUnlocked(AmmoType type)
    {
        if (type == ammoType)
            UpdateActiveState(); // ✅ vừa nhặt súng là bật ammo ngay
    }

    void UpdateActiveState()
    {
        bool unlocked =
            WeaponUnlockManager.Instance != null &&
            WeaponUnlockManager.Instance.IsAmmoUnlocked(ammoType);

        if (mesh != null) mesh.enabled = unlocked;
        if (col != null) col.enabled = unlocked;

        canPickup = unlocked;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!canPickup) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null || player.currentWeapon == null) return;

        WeaponBase weapon = player.currentWeapon;

        if (weapon.ammoType != ammoType) return;

        if (weapon.reserveAmmo >= weapon.maxReserveAmmo) return;

        weapon.reserveAmmo += ammoAmount;
        if (weapon.reserveAmmo > weapon.maxReserveAmmo)
            weapon.reserveAmmo = weapon.maxReserveAmmo;

        player.UpdateUI();
        StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {
        canPickup = false;

        if (mesh != null) mesh.enabled = false;
        if (col != null) col.enabled = false;

        yield return new WaitForSeconds(respawnTime);

        UpdateActiveState();
    }
}
