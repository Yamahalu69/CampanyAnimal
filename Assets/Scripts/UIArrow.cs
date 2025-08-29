using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class UIArrow : MonoBehaviour
{
    [SerializeField, Header("タスク位置案内")] private Transform taskpos = default;
    private GameObject maincameraGO;
    [SerializeField] private Image arrow = default;
    private GameObject pos;
    private RectTransform rectTransform;

    [SerializeField] private Camera maincamera;

    private Image Image;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Image = GetComponent<Image>();
        var list = GameManager.instance.taskManager.TaskGOs();
        maincameraGO = GameObject.Find("Main Camera");
    }

    private void Update()
    {

        if (pos == null)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!taskpos) return;

        if (maincamera == null)
        {
            bool b = maincameraGO.TryGetComponent<Camera>(out maincamera);
        }

        float canvasscale = transform.root.localScale.z;
        var center =new Vector3(Screen.width, Screen.height) * 0.5f;

        var pos = maincamera.WorldToScreenPoint(taskpos.position) - center;

        if (pos.z < 0f)
        {
            pos.x = -pos.x;
            pos.y = -pos.y;

            if (Mathf.Approximately(pos.y, 0f))
            {
                pos.y = -center.y;
            }
        }

        var halfsize = 0.5f * canvasscale * rectTransform.sizeDelta;
        float d = Mathf.Max(
            Mathf.Abs(pos.x / (center.x - halfsize.x)),
            Mathf.Abs(pos.y / (center.y - halfsize.y))
        );

        bool isoffscreen = d > 1f;
        if (isoffscreen)
        {
            pos.x /= d;
            pos.y /= d;
        }

        rectTransform.anchoredPosition = pos / canvasscale;

        arrow.enabled = true;
        arrow.rectTransform.eulerAngles=isoffscreen
            ?new Vector3(0f,0f,Mathf.Atan2(pos.y,pos.x)* Mathf.Rad2Deg)
            :Vector3.zero;

    }

    //高田追加分
    public void SetTargetObject(GameObject target)
    {
        pos = target;
        taskpos = target.transform;
    }
}
