using UnityEngine;

public class npcdp : MonoBehaviour
{
    public GameObject npcPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 10f; // 何秒ごとに増やすか

    private void Start()
    {
        InvokeRepeating(nameof(SpawnNPC), 0f, spawnInterval);
    }

    public void SpawnNPC()
    {
        GameObject npc = Instantiate(npcPrefab, spawnPoint.position, spawnPoint.rotation);

        // NPC にスポーン地点を伝える
        npc_move controller = npc.GetComponent<npc_move>();
        if (controller != null)
        {
            controller.SetSpawnPoint(spawnPoint.position);
        }
    }
}
