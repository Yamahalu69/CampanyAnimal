using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NewNPCManager : MonoBehaviour
{
    public Transform[] points;  // NPCが移動するターゲットポイント
    public float waitTime = 2f; // ターゲットに到達後の待機時間
    private NavMeshAgent agent; // NavMeshAgentコンポーネント
    private int currentPointIndex = 0; // 現在のターゲットポイント
    private bool isMoving = false; // NPCが移動中かどうか
    public Transform destination;

    void Start()
    {
        // NavMeshAgentコンポーネントを取得
        agent = GetComponent<NavMeshAgent>();

        if (points.Length > 0)
        {
            // 初期ターゲットを設定
            SetRandomTarget();
        }
    }

    void Update()
    {
        // NPCが移動中でない場合のみ、移動を開始
        if (!isMoving && points.Length > 0)
        {
            StartCoroutine(MoveToTargetWithDelay());
        }
    }

    // NPCがターゲットに向かって移動し、待機するコルーチン
    IEnumerator MoveToTargetWithDelay()
    {
        isMoving = true;

        // 現在のターゲットをNavMeshAgentで設定
        agent.SetDestination(points[currentPointIndex].position);

        // ターゲットに到達したかどうかをチェック
        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null; // 移動中は毎フレーム待機
        }

        // ターゲットに到達した後、待機時間を追加
        yield return new WaitForSeconds(waitTime);

        // 次のターゲットを設定
        SetRandomTarget();

        // 移動再開
        isMoving = false;
    }

    // ランダムにターゲットを選択
    void SetRandomTarget()
    {
        currentPointIndex = Random.Range(0, points.Length);
    }
    // NPCを指定された位置に移動させるメソッド
    public void MoveToPosition()
    {
        agent.SetDestination(destination.position);
    }
}
