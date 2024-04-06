using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private int HP = 100;
    [SerializeField] private float approachSpeed = 5f; // Velocidad de aproximación
    private Animator animator;
    private NavMeshAgent navAgent;
    private Transform playerTransform; // Agrega una referencia al transform del jugador

    private void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();

        // Configura la velocidad de aproximación
        navAgent.speed = approachSpeed;

        // Configura la prioridad de evasión de obstáculos
        navAgent.avoidancePriority = 50;

        // Busca el jugador por nombre
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player GameObject not found.");
        }
    }

    public void TakeDamage(int damageAmount = 1)
    {
        HP -= damageAmount;

        if (HP <= 0)
        {
            animator.SetTrigger("DIE");
            Destroy(gameObject);
        }
        else
        {
            animator.SetTrigger("DAMAGE");
        }
        if (HP <= 0)
        {
            animator.SetTrigger("DIE");
            EnemyCounter.instance.EnemyDefeated(); // Notificar al EnemyCounter que un enemigo fue derrotado
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            // Mueve el agente hacia la posición del jugador
            navAgent.SetDestination(playerTransform.position);
        }

        if (navAgent.velocity.magnitude > 0.1f)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
