using UnityEngine;
using UnityEngine.AI;

public class ZombieHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    private Animator animator;
    private NavMeshAgent agent;
    public bool isDead = false;
    ZombieSpawner spawner;

    public float deathDelay; // thời gian sau khi chết mới xoá

    public int scoreReward = 10;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        spawner = FindFirstObjectByType<ZombieSpawner>();

    }

    // Hàm gọi để zombie nhận sát thương
    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage! Current HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.AddScore(scoreReward);
        }

        // Dừng NavMeshAgent
        if (agent != null)
            agent.isStopped = true;

            animator.SetBool("Die",true);

        if (spawner != null)
            spawner.ZombieDied();


        // Xoá zombie sau vài giây
        Destroy(gameObject, deathDelay);
    }
}
