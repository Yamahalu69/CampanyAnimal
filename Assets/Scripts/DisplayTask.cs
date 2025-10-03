using UnityEngine;
using UnityEngine.UI;

public class DisplayTask : MonoBehaviour
{
    [SerializeField] private GameObject[] poteti;
    [SerializeField] private GameObject[] potetiefect;

    

    private int taskcount = 0;

    [SerializeField] private Display task;

    [SerializeField] private Taskcamera taskcamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        foreach (var gobj in poteti)
        {
            gobj.SetActive(false);
        }

        foreach (var pe in potetiefect)
        {
            pe.SetActive(false);
        }

    }
   

    
    // Update is called once per frame
    void Update()
    {
        potetiefect[taskcount].SetActive(true);
        if (task.crear)
        {
            poteti[taskcount].SetActive(true);
            potetiefect[taskcount].SetActive(false);
            taskcount++;
            task.crear = false;
            
        }
        
    }

    
}
