using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NewNPCManager : MonoBehaviour
{
    public Transform[] points;  // NPC���ړ�����^�[�Q�b�g�|�C���g
    public float waitTime = 2f; // �^�[�Q�b�g�ɓ��B��̑ҋ@����
    private NavMeshAgent agent; // NavMeshAgent�R���|�[�l���g
    private int currentPointIndex = 0; // ���݂̃^�[�Q�b�g�|�C���g
    private bool isMoving = false; // NPC���ړ������ǂ���
    public Transform destination;

    void Start()
    {
        // NavMeshAgent�R���|�[�l���g���擾
        agent = GetComponent<NavMeshAgent>();

        if (points.Length > 0)
        {
            // �����^�[�Q�b�g��ݒ�
            SetRandomTarget();
        }
    }

    void Update()
    {
        // NPC���ړ����łȂ��ꍇ�̂݁A�ړ����J�n
        if (!isMoving && points.Length > 0)
        {
            StartCoroutine(MoveToTargetWithDelay());
        }
    }

    // NPC���^�[�Q�b�g�Ɍ������Ĉړ����A�ҋ@����R���[�`��
    IEnumerator MoveToTargetWithDelay()
    {
        isMoving = true;

        // ���݂̃^�[�Q�b�g��NavMeshAgent�Őݒ�
        agent.SetDestination(points[currentPointIndex].position);

        // �^�[�Q�b�g�ɓ��B�������ǂ������`�F�b�N
        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null; // �ړ����͖��t���[���ҋ@
        }

        // �^�[�Q�b�g�ɓ��B������A�ҋ@���Ԃ�ǉ�
        yield return new WaitForSeconds(waitTime);

        // ���̃^�[�Q�b�g��ݒ�
        SetRandomTarget();

        // �ړ��ĊJ
        isMoving = false;
    }

    // �����_���Ƀ^�[�Q�b�g��I��
    void SetRandomTarget()
    {
        currentPointIndex = Random.Range(0, points.Length);
    }
    // NPC���w�肳�ꂽ�ʒu�Ɉړ������郁�\�b�h
    public void MoveToPosition()
    {
        agent.SetDestination(destination.position);
    }
}
