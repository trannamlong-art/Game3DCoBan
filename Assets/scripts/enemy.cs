using UnityEngine;
using UnityEngine.AI;

public class EnemyWave : MonoBehaviour
{
    public float attackRange = 1.5f;
    public float attackDamage = 15f;
    public float attackRate = 1.2f;

    private float nextAttackTime = 0f;

    [Header("Audio")]
    public AudioSource audioRun;
    public AudioSource audioAttack;

    private NavMeshAgent agent;
    private Animator animator;
    public Transform target;  // Player, gán ngay khi spawn

    private ZombieHealth Isdead; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        Isdead = GetComponent<ZombieHealth>();

        if (audioRun != null)
        {
            audioRun.loop = true;       // ✅ tiếng chạy lặp
            audioRun.playOnAwake = false;
            audioRun.Stop();
        }

        if (audioAttack != null)
        {
            audioAttack.loop = false;  // ✅ mỗi cú đánh 1 tiếng
            audioAttack.playOnAwake = false;
            audioAttack.Stop();
        }

        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                target = playerObj.transform;
        }
    }

    void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (Isdead.isDead)
        {
            // Nếu đã chết thì không làm gì cả
            agent.isStopped = true;
            animator.SetBool("Run", false);
            animator.SetBool("Attack", false);

            if (audioRun != null && audioRun.isPlaying)
                audioRun.Stop();
        }
        else if (distance > attackRange)
        {
            // Chạy về player
            agent.isStopped = false;
            agent.SetDestination(target.position);

            animator.SetBool("Run", true);
            animator.SetBool("Attack", false);

            if (audioRun != null && !audioRun.isPlaying)
                audioRun.Play();
        }
        
        else
        {
            if (audioRun != null && audioRun.isPlaying)
                audioRun.Stop();

            // Tấn công
            agent.isStopped = true;

            animator.SetBool("Run", false);
            animator.SetBool("Attack", true);

            if (Time.time >= nextAttackTime)
            {
                nextAttackTime = Time.time + attackRate;
                Attack();
            }
        }
    }

    void Attack()
    {
        if (target == null) return;

        if (audioAttack != null)
        {
            audioAttack.Stop();   // tránh chồng tiếng
            audioAttack.Play();
        }

        PlayerController player = target.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(attackDamage); // ✅ GÂY DAMAGE THẬT
        }

        Debug.Log("Zombie tấn công! Damage: " + attackDamage);
    }

}
