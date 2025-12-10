using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    public GameObject weaponSlot1;
    public GameObject weaponSlot2;
    public GameObject weaponSlot3;
    public GameObject weaponSlot4;

    public TMP_Text weaponNameText;

    private int currentSlot = 0; // 0 = chưa cầm súng

    private PlayerController player;

    void Start()
    {
        player = FindFirstObjectByType<PlayerController>();

        // ✅ KHÔNG equip gì khi vào game
        EquipSlot(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            EquipSlot(1);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            EquipSlot(2);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            EquipSlot(3);

        if (Input.GetKeyDown(KeyCode.Alpha4))
            EquipSlot(4);
    }

    public void EquipSlot(int slot)
    {
        currentSlot = slot;

        if (weaponSlot1 != null)
            weaponSlot1.SetActive(false);

        if (weaponSlot2 != null)
            weaponSlot2.SetActive(false);

        if (weaponSlot3 != null)
            weaponSlot3.SetActive(false);

        if (weaponSlot4 != null)
            weaponSlot4.SetActive(false);

        GameObject activeWeapon = null;

        if (slot == 1 && weaponSlot1 != null)
            activeWeapon = weaponSlot1;

        if (slot == 2 && weaponSlot2 != null)
            activeWeapon = weaponSlot2;

        if (slot == 3 && weaponSlot3 != null)
            activeWeapon = weaponSlot3;

        if (slot == 4 && weaponSlot4 != null)
            activeWeapon = weaponSlot4;

        if (activeWeapon != null)
        {
            activeWeapon.SetActive(true);

            WeaponBase weapon = activeWeapon.GetComponent<WeaponBase>();
            if (weapon != null && player != null)
                player.currentWeapon = weapon;
                player.UpdateUI();

            if (weaponNameText != null)
                weaponNameText.text = activeWeapon.name.Replace("(Clone)", "");
        }
        else
        {
            // ✅ Không cầm súng
            if (player != null)
                player.currentWeapon = null;

            if (weaponNameText != null)
                weaponNameText.text = "";
        }
    }

    // ✅ Khi nhặt vũ khí
    public void AddWeapon(GameObject newWeapon)
    {
        // ✅ Slot 1 trống → vào Slot 1
        if (weaponSlot1 == null)
        {
            weaponSlot1 = newWeapon;
            GanParentVuKhi(newWeapon);
            WeaponBase weapon = newWeapon.GetComponent<WeaponBase>();
            if (weapon != null)
            {
                WeaponUnlockManager.Instance.UnlockAmmo(weapon.ammoType);
            }
            EquipSlot(1);
            return;
        }

        // ✅ Slot 2 trống → vào Slot 2
        if (weaponSlot2 == null)
        {
            weaponSlot2 = newWeapon;
            GanParentVuKhi(newWeapon);
            WeaponBase weapon = newWeapon.GetComponent<WeaponBase>();
            if (weapon != null)
            {
                WeaponUnlockManager.Instance.UnlockAmmo(weapon.ammoType);
            }
            EquipSlot(2);
            return;
        }

        // ✅ Slot 2 trống → vào Slot 2
        if (weaponSlot3 == null)
        {
            weaponSlot3 = newWeapon;
            GanParentVuKhi(newWeapon);
            WeaponBase weapon = newWeapon.GetComponent<WeaponBase>();
            if (weapon != null)
            {
                WeaponUnlockManager.Instance.UnlockAmmo(weapon.ammoType);
            }
            EquipSlot(3);
            return;
        }

        // ✅ Slot 2 trống → vào Slot 2
        if (weaponSlot4 == null)
        {
            weaponSlot4 = newWeapon;
            GanParentVuKhi(newWeapon);
            WeaponBase weapon = newWeapon.GetComponent<WeaponBase>();
            if (weapon != null)
            {
                WeaponUnlockManager.Instance.UnlockAmmo(weapon.ammoType);
            }
            EquipSlot(4);
            return;
        }

        Debug.Log("Đã đủ 4 súng!");
    }

    void GanParentVuKhi(GameObject weapon)
    {
        // ✅ Gán vào tay player hoặc holder
        weapon.transform.SetParent(transform);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        weapon.SetActive(false);
    }
}
