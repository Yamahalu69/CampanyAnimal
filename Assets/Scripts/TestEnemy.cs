using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class TestEnemy : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    Transform target;

    [SerializeField]
    List<Transform> pointTransform = new List<Transform>();

    private Transform targetPos;

    void Start()
    {
        targetPos = RandomTransform();
    }

    void Update()
    {
        agent.SetDestination(targetPos.position);
        float x = Mathf.Abs(targetPos.position.x - transform.position.x);
        float z = Mathf.Abs(targetPos.position.z - transform.position.z);

        if (x < 0.5f && z < 0.5f)
        {
            targetPos = RandomTransform();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            targetPos = target;
        }
    }

    Transform RandomTransform()
    {
        Transform pos = pointTransform[Random.Range(0, pointTransform.Count)];

        return pos;
    }
}
