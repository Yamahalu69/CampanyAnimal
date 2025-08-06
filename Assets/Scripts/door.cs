using UnityEngine;

public class door : MonoBehaviour
{
    [SerializeField] private Transform dor;
    [SerializeField,Header("開く角度")] private float openangle = 75f;
    [SerializeField,Header("開く速度")] private float openspeed = 2.0f;

    private Quaternion close;
    private Quaternion target;
    private bool open = false;
    private int count = 0;

    void Start()
    {
        close = dor.localRotation;
        target = close;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        count++;

        Vector3 direction = other.transform.position - transform.position;
        float dot = Vector3.Dot(transform.forward,direction.normalized);

        float angle = (dot > 0) ? openangle : -openangle;
        target = Quaternion.Euler(0f, angle, 0f);
        open = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.CompareTag("Player")) return;
        count = Mathf.Max(0, count - 1);

        if(count == 0)
        {
            target = close;
            open = true;
        }
    }

    void Update()
    {
        if(open)
        {
            dor.localRotation = Quaternion.Slerp(dor.localRotation, target, Time.deltaTime * openspeed);


            if(Quaternion.Angle(dor.localRotation,target)<1f)
            {
                dor.localRotation = target;
                open = false;
            }
        }
    }
}
