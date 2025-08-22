using UnityEngine;

public class Taskcamera : MonoBehaviour
{
    [SerializeField] private GameObject maincamera;
    [SerializeField] private GameObject subcamera;

    [SerializeField] private GameObject displaytask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maincamera = GameObject.Find("Main Camera");
        subcamera = GameObject.Find("Sub Camera");
        subcamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(displaytask == true)
        {
            maincamera.SetActive (false);
            subcamera.SetActive (true);
        }
        else
        {
            maincamera.SetActive (true);
            subcamera.SetActive (false);
        }
    }
}
