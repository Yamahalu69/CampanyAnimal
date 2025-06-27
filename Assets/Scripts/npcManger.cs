using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npcManger : MonoBehaviour
{
    public Transform[] randomPoints;      // ランダム移動用ポイント
    public Transform queuePoint;          // 待機列のスタート地点
    public Transform workPoint;           // 実際に仕事する場所（列の先頭の場所）
    public Transform removePoint;         // 消える場所
    public float waitTime = 2f;           // 待機時間や処理時間
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
                    // ランダム移動が終わったらキューに並ぶ処理へ
                    GoToQueue();
                }
                break;

            case State.GoingToQueue:
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    // キュー列の最後尾に追加
                    queue.Enqueue(this);
                    currentState = State.WaitingInQueue;
                }
                break;

            case State.WaitingInQueue:
                // 自分がキューの先頭かどうかチェック
                if (queue.Peek() == this)
                {
                    // 列の先頭ならワークポイントへ移動
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
                    // 到達したら消える（例: 非アクティブにする）
                    currentState = State.Removed;
                    gameObject.SetActive(false);
                }
                break;

            case State.Removed:
                // 何もしない
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

        // 処理中の例として待機
        yield return new WaitForSeconds(waitTime);

        // ワーク終了後、キューから自分を除く
        if (queue.Count > 0 && queue.Peek() == this)
        {
            queue.Dequeue();
        }

        // 消去ポイントへ移動開始
        agent.SetDestination(removePoint.position);
        currentState = State.GoingToRemove;
    }
}
