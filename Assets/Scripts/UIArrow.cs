using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class UIArrow : MonoBehaviour
{
    [Header("タスク位置案内")]
    [SerializeField] private Transform taskpos =default;
    [SerializeField] private Camera maincamera;
    [SerializeField] private Image arrow = default;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var list = GameManager.instance.taskManager.TaskGOs();
        maincamera=Camera.main;
    }

    private void Update()
    {
       
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!taskpos) return;

        float canvasscale = transform.root.localScale.z;
        var center = 0.5f * new Vector3(Screen.width, Screen.height)*0.5f;

        var pos = maincamera.WorldToScreenPoint(taskpos.position)-center;

        if(pos.z<0f)
        {
            pos.x = -pos.x;
            pos.y = -pos.y;

        }

        var halfsize = 0.5f*canvasscale*rectTransform.sizeDelta;
        float d  = Mathf.Max(
            Mathf.Abs(pos.x/(center.x-halfsize.x)),
            Mathf.Abs(pos.y/(center.y-halfsize.y))
        );

        bool isoffscreen = (pos.x < 0f || d > 1f);
        if(isoffscreen)
        {
            pos.x /= d;
            pos.y /= d;
        }

        rectTransform.anchoredPosition = pos/canvasscale;
        if(isoffscreen)
        {
            Vector2 direction = pos.normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            arrow.rectTransform.eulerAngles = new Vector3(0f, 0f, angle);
        }
        else
        {
            arrow.rectTransform.eulerAngles = Vector3.zero;
        }

         arrow.enabled = true;
    }
}
