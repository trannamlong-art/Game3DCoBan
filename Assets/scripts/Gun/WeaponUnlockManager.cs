using System.Collections.Generic;
using UnityEngine;

public class WeaponUnlockManager : MonoBehaviour
{
    public static WeaponUnlockManager Instance;

    private HashSet<AmmoType> unlockedAmmoTypes = new HashSet<AmmoType>();

    public delegate void OnAmmoUnlock(AmmoType type);
    public event OnAmmoUnlock OnAmmoUnlocked; // ✅ EVENT

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void UnlockAmmo(AmmoType type)
    {
        if (!unlockedAmmoTypes.Contains(type))
        {
            unlockedAmmoTypes.Add(type);
            OnAmmoUnlocked?.Invoke(type); // ✅ BÁO CHO AMMO PICKUP BIẾT
        }
    }

    public bool IsAmmoUnlocked(AmmoType type)
    {
        return unlockedAmmoTypes.Contains(type);
    }
}
