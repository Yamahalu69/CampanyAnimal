using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Taskcamera : MonoBehaviour
{
    [SerializeField] private GameObject maincamera;
    [SerializeField] private GameObject displaycamera;
    [SerializeField] private GameObject rejistercamera;

    [SerializeField] private GameObject displaytask;
    [SerializeField] RegisterTask rejistertask;

    [SerializeField] private Canvas UiArrow;

    [SerializeField] private Display task;

    [SerializeField] private Image playerimage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerimage.enabled = false;
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
            playerimage.enabled = true;
        }
        else if(task.crear)
        {
            
            StartCoroutine(Delay(3f));

            
        }
        
        else if(Input.GetKeyUp(KeyCode.Space))
        {
             maincamera.GetComponent<Camera>().enabled = true;

             UiArrow.enabled = true;

            playerimage.enabled = false;
        }
        
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);

        displaycamera.GetComponent<Camera>().enabled = false;

        maincamera.GetComponent<Camera>().enabled = true;

        UiArrow.enabled = true;
    }
    public void Rejistercamera()
    {
        maincamera.GetComponent<Camera>().enabled = true;
        rejistercamera.GetComponent<Camera>().enabled = false;
        UiArrow.enabled = true;
        playerimage.enabled = false;
    }
    
}
