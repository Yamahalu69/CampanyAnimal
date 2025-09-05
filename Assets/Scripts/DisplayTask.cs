using UnityEngine;
using UnityEngine.UI;

public class DisplayTask : MonoBehaviour
{
    [SerializeField] private GameObject[] poteti;
    //[SerializeField] private GameObject[] potetiefect;

    public Transform[] target;
    private LineRenderer line;

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
        //foreach (var pe in potetiefect)
        //{
        //    pe.SetActive(false);
        //}

        line = GetComponent<LineRenderer>();
        line.positionCount = 5;
        line.loop = false;
        line.useWorldSpace = true;
        line.widthMultiplier = 0.05f;

        UpdateOutline();
    }
   

    
    // Update is called once per frame
    void Update()
    {
        if (task.crear)
        {
            poteti[taskcount].SetActive(true);
            //potetiefect[taskcount].SetActive(true);
            taskcount++;
            task.crear = false;
            taskcamera.OnCameraInvoke();
        }
        
    }

    void UpdateOutline()
    {
        Bounds bounds = target[taskcount].GetComponent<Renderer>().bounds;

        Vector3[] points = new Vector3[5];
        points[0] = new Vector3(bounds.min.x, bounds.min.y, bounds.min.z);
        points[1] = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
        points[2] = new Vector3(bounds.max.x, bounds.max.y, bounds.min.z);
        points[3] = new Vector3(bounds.min.x, bounds.max.y, bounds.min.z);
        points[4] = points[0];

        line.SetPositions(points);
    }
}
