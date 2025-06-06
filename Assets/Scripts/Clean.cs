using UnityEngine;

public class Clean : MonoBehaviour
{
    private KeyCode keycount = KeyCode.Return;

    private int keypress = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keycount))
        {
            keypress++;
            if (keypress == 10)
            {
                Debug.Log("èIÇÌÇË");
            }
        }
    }
}
