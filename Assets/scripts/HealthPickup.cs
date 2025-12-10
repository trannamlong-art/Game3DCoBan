using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour
{
    public float healAmount = 25f;     // ✅ Hồi bao nhiêu máu
    public float respawnTime = 15f;    // ✅ Thời gian hồi lại

    private bool canPickup = true;
    private MeshRenderer mesh;
    private Collider col;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!canPickup) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;

        // ✅ Nếu đã full máu thì không nhặt
        if (player.healthBar != null && player.healthBar.value >= player.maxHealth)
            return;

        // ✅ HỒI MÁU
        player.TakeDamage(-healAmount); // dùng damage âm để hồi

        StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {
        canPickup = false;

        if (mesh != null) mesh.enabled = false;
        if (col != null) col.enabled = false;

        yield return new WaitForSeconds(respawnTime);

        if (mesh != null) mesh.enabled = true;
        if (col != null) col.enabled = true;

        canPickup = true;
    }
}
