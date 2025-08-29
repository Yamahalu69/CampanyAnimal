using UnityEngine;
using UnityEngine.UI;

public class DisplayTask : MonoBehaviour
{
    [SerializeField] private GameObject[] poteti;
    private int taskcount = 0;
    [SerializeField] private GameObject Display;
    private Display task;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Display.SetActive(false);

        foreach (var gobj in poteti)
        {
            gobj.SetActive(false);
        }
    }
   

    
    // Update is called once per frame
    void Update()
    {
        //if( )
        //{
        //    poteti[taskcount].SetActive(true);
        //}
        //else
        //{
        //    taskcount++;
        //}
    }
}
