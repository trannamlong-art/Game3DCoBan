using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WeaponManager wm = other.GetComponent<WeaponManager>();
            if (wm != null)
            {
                GetComponent<Collider>().enabled = false;

                gameObject.name = gameObject.name.Replace("(Clone)", "");

                wm.AddWeapon(gameObject);
            }
        }
    }
}
