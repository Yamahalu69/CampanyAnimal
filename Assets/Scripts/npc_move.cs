using UnityEngine;
using UnityEngine.AI;

public class npc_move : MonoBehaviour
{
    private Vector3 spawnPosition;
    private NavMeshAgent agent;
    private bool returning = false;

    public float returnDelay = 30f;
    public float disappearDistance = 0.5f;
    private Animator ani;

    void Start()
    {
        ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        Invoke(nameof(ReturnToSpawn), returnDelay);
    }

    public void SetSpawnPoint(Vector3 position)
    {
        spawnPosition = position;
    }

    void ReturnToSpawn()
    {
        if (agent != null)
        {
            agent.SetDestination(spawnPosition);
            returning = true;
        }
    }

    void Update()
    {
        float speed = agent.velocity.magnitude;
        ani.SetFloat("Speed",speed);

        if (returning)
        {
            float distance = Vector3.Distance(transform.position, spawnPosition);
            if (distance <= disappearDistance)
            {
                Destroy(gameObject);
            }
        }
    }
    
}
