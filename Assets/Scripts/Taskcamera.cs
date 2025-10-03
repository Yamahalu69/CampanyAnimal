using UnityEngine;

public class Taskcamera : MonoBehaviour
{
    [SerializeField] private GameObject maincamera;
    [SerializeField] private GameObject displaycamera;
    [SerializeField] private GameObject rejistercamera;

    [SerializeField] private GameObject displaytask;
    [SerializeField] RegisterTask rejistertask;

    [SerializeField] private Canvas UiArrow;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UiArrow.enabled = true;
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
            UiArrow.enabled = false;
        }

        else if (rejistertask.isPlaying)
        {
            maincamera.GetComponent<Camera>().enabled = false;
            rejistercamera.GetComponent<Camera>().enabled = true;
            UiArrow.enabled = false;
        }
        else
        {
            maincamera.GetComponent<Camera>().enabled = true;
            displaycamera.GetComponent<Camera>().enabled = false;
            rejistercamera.GetComponent<Camera>().enabled = false;
            UiArrow.enabled = true;
        }
        
    }

    public void Rejistercamera()
    {
        maincamera.GetComponent<Camera>().enabled = true;
        rejistercamera.GetComponent<Camera>().enabled = false;
        UiArrow.enabled = true;
    }

    public void OnCameraInvoke()
    {
        Invoke("OnCamera", 1.5f);
    }

    
}
