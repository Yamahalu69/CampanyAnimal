using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class UIArrow : MonoBehaviour
{
    [SerializeField, Header("タスク位置案内")] private Transform taskpos=default;
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

    // Update is called once per frame
    void LateUpdate()
    {
        float canvasscale = transform.root.localScale.z;
        var center = 0.5f * new Vector3(Screen.width, Screen.height);

        var pos = maincamera.WorldToScreenPoint(taskpos.position)-center;

        if(pos.z<0f)
        {
            pos.x = -pos.x;
            pos.y = -pos.y;

            if(Mathf.Approximately(pos.y,0f))
            {
                pos.y=-center.y;
            }
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

        arrow.enabled = isoffscreen;
        if(isoffscreen)
        {
            arrow.rectTransform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg);
        }

    }
}
