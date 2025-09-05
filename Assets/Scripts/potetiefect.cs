using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class potetiefect : MonoBehaviour
{
    public Transform target;
    private LineRenderer line;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 5;
        line.loop = false;
        line.useWorldSpace = true;
        line.widthMultiplier = 0.05f;

        UpdateOutline();
    }

    // Update is called once per frame
    void UpdateOutline()
    {
        Bounds bounds = target.GetComponent<Renderer>().bounds;

        Vector3[] points = new Vector3[5];
        points[0] = new Vector3(bounds.min.x, bounds.min.y, bounds.min.z);
        points[1] = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
        points[2] = new Vector3(bounds.max.x, bounds.max.y, bounds.min.z);
        points[3] = new Vector3(bounds.min.x, bounds.max.y, bounds.min.z);
        points[4] = points[0];

        line.SetPositions(points);
    }
}
