using UnityEngine;

public class NPCCommand : MonoBehaviour
{
    public NewNPCManager npcController;  // NPC�̈ړ��𐧌䂷��X�N���v�g

    // Update is called once per frame
    void Update()
    {

        // "F"�L�[�������ƁANPC���w�肵�����W�Ɉړ�������
        if (Input.GetKeyDown(KeyCode.F))
        {
            npcController.MoveToPosition();
            Debug.Log("NPC���w��ʒu�Ɉړ������܂���: ");
        }
    }
}
