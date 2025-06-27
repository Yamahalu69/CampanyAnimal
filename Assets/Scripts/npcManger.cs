using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npcManger : MonoBehaviour
{
    public Transform[] randomPoints;      // �����_���ړ��p�|�C���g
    public Transform queuePoint;          // �ҋ@��̃X�^�[�g�n�_
    public Transform workPoint;           // ���ۂɎd������ꏊ�i��̐擪�̏ꏊ�j
    public Transform removePoint;         // ������ꏊ
    public float waitTime = 2f;           // �ҋ@���Ԃ⏈������
    private NavMeshAgent agent;

    private static Queue<npcManger> queue = new Queue<npcManger>();

    private enum State
    {
        MovingRandom,
        GoingToQueue,
        WaitingInQueue,
        Working,
        GoingToRemove,
        Removed
    }

    private State currentState = State.MovingRandom;

    private bool isWorkingDone = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MoveToRandomPoint();
    }

    void Update()
    {
        switch (currentState)
        {
            case State.MovingRandom:
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    // �����_���ړ����I�������L���[�ɕ��ԏ�����
                    GoToQueue();
                }
                break;

            case State.GoingToQueue:
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    // �L���[��̍Ō���ɒǉ�
                    queue.Enqueue(this);
                    currentState = State.WaitingInQueue;
                }
                break;

            case State.WaitingInQueue:
                // �������L���[�̐擪���ǂ����`�F�b�N
                if (queue.Peek() == this)
                {
                    // ��̐擪�Ȃ烏�[�N�|�C���g�ֈړ�
                    currentState = State.Working;
                    agent.SetDestination(workPoint.position);
                }
                break;

            case State.Working:
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!isWorkingDone)
                    {
                        StartCoroutine(DoWork());
                    }
                }
                break;

            case State.GoingToRemove:
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    // ���B�����������i��: ��A�N�e�B�u�ɂ���j
                    currentState = State.Removed;
                    gameObject.SetActive(false);
                }
                break;

            case State.Removed:
                // �������Ȃ�
                break;
        }
    }

    private void MoveToRandomPoint()
    {
        if (randomPoints.Length == 0) return;
        int idx = Random.Range(0, randomPoints.Length);
        agent.SetDestination(randomPoints[idx].position);
        currentState = State.MovingRandom;
    }

    private void GoToQueue()
    {
        agent.SetDestination(queuePoint.position);
        currentState = State.GoingToQueue;
    }

    private IEnumerator DoWork()
    {
        isWorkingDone = true;

        // �������̗�Ƃ��đҋ@
        yield return new WaitForSeconds(waitTime);

        // ���[�N�I����A�L���[���玩��������
        if (queue.Count > 0 && queue.Peek() == this)
        {
            queue.Dequeue();
        }

        // �����|�C���g�ֈړ��J�n
        agent.SetDestination(removePoint.position);
        currentState = State.GoingToRemove;
    }
}
