using UnityEngine;

public class LinkUIArrowWithTorus : MonoBehaviour
{
    [Header("–îˆóUIƒvƒŒƒnƒu")]
    public GameObject display, cleaning, stocking, register;

    [SerializeField]
    private Task task;


    void Awake()
    {
        var canvas = GameObject.Find("Canvas");
        GameObject obj;
        switch (task)
        {
            case Task.display:
                obj = Instantiate(display);
                obj.transform.SetParent(canvas.transform);
                obj.GetComponent<UIArrow>().pos = this.gameObject;
                break;
            case Task.cleaning:
                obj = Instantiate(cleaning);
                obj.transform.SetParent(canvas.transform);
                obj.GetComponent<UIArrow>().pos = this.gameObject;
                break;
            case Task.register:
                obj = Instantiate(register);
                obj.transform.SetParent(canvas.transform);
                obj.GetComponent<UIArrow>().pos = this.gameObject;
                break;
            case Task.stocking:
                obj = Instantiate(stocking);
                obj.transform.SetParent(canvas.transform);
                obj.GetComponent<UIArrow>().pos = this.gameObject;
                break;
        }
    }
}
