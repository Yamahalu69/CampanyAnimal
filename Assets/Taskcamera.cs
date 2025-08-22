using UnityEngine;

public class Taskcamera : MonoBehaviour
{
    [SerializeField] private GameObject maincamera;
    [SerializeField] private GameObject displaycamera;
    [SerializeField] private GameObject rejistercamera;

    [SerializeField] private GameObject displaytask;
    [SerializeField] RegisterTask rejistertask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maincamera.GetComponent<Camera>().enabled = true;
        displaycamera.GetComponent<Camera>().enabled = false;
        rejistercamera.GetComponent<Camera>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (displaytask.activeSelf)
        {
            maincamera.GetComponent<Camera>().enabled = false;
            displaycamera.GetComponent<Camera>().enabled = true;
        }
        else if (rejistertask.isPlaying)
        {
            maincamera.GetComponent<Camera>().enabled = false;
            rejistercamera.GetComponent<Camera>().enabled = true;
        }
        else
        {
            maincamera.GetComponent<Camera>().enabled = true;
            rejistercamera.GetComponent<Camera>().enabled = false;
            displaycamera.GetComponent <Camera>().enabled = false;
        }
    }
}
