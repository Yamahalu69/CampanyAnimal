using UnityEngine;

public class NPCCommand : MonoBehaviour
{
    public NewNPCManager npcController;  // NPCの移動を制御するスクリプト

    // Update is called once per frame
    void Update()
    {

        // "F"キーを押すと、NPCを指定した座標に移動させる
        if (Input.GetKeyDown(KeyCode.F))
        {
            npcController.MoveToPosition();
            Debug.Log("NPCを指定位置に移動させました: ");
        }
    }
}
