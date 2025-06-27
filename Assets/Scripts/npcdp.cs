using UnityEngine;

public class npcdp : MonoBehaviour
{
    public GameObject npcPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 10f; // ���b���Ƃɑ��₷��

    private void Start()
    {
        InvokeRepeating(nameof(SpawnNPC), 0f, spawnInterval);
    }

    public void SpawnNPC()
    {
        GameObject npc = Instantiate(npcPrefab, spawnPoint.position, spawnPoint.rotation);

        // NPC �ɃX�|�[���n�_��`����
        npc_move controller = npc.GetComponent<npc_move>();
        if (controller != null)
        {
            controller.SetSpawnPoint(spawnPoint.position);
        }
    }
}
