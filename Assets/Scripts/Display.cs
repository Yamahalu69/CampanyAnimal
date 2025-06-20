using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    [SerializeField] Image bar;//移動するバー
    [SerializeField] Image goal;//止める位置
    [SerializeField] private float movedistance = 200f;//高さ
    [SerializeField, Header("バーの上昇スピード")] private float movesp = 2f;//上昇スピード
    [SerializeField, Header("バーの下降スピード")] private float downsp = 2f;//加工スピード

    private Vector2 farstpos;
    private float curr = 0f;
    private bool enterlock = false;//下降中かどうか
    private bool islock = false;//入力がロックされているか
    private bool clear = false;//ゴールにバーがあるかどうか

    private void Start()
    {
        if (bar != null)
        {
            farstpos = bar.rectTransform.anchoredPosition;
            bar.rectTransform.anchoredPosition = farstpos;
        }
    }

    private bool Ingoal()
    {
        RectTransform goalrt = goal.rectTransform;
        RectTransform barrt = bar.rectTransform;

        float bary = barrt.anchoredPosition.y;
        float goaly = goalrt.anchoredPosition.y;
        float goalheight = goalrt.rect.height;

        float goaltop = goaly + goalheight / 2f;
        float goalbottom = goaly - goalheight / 2f;

        return bary >= goalbottom && bary <= goaltop;
    }

    private void Update()
    {
        if (clear) return;

        if(!enterlock &&islock&&Ingoal())
        {
            clear = true;
            this.gameObject.SetActive(false);
        }
        //バー上昇
        if (!islock && !enterlock && Input.GetKey(KeyCode.Return))
        {
            curr += movesp * Time.deltaTime;

            if (curr >= movedistance)
            {
                curr = movedistance;
                enterlock = true;
                islock = true;
            }
        }

        //Enterキーを離すと下降開始
        if (!islock && !Input.GetKey(KeyCode.Return) && curr > 0f)
        {
            enterlock = true;
            islock = true;
        }

        //バーが下降中
        if (enterlock)
        {
            curr -= downsp * Time.deltaTime;
            if (curr <= 0f)
            {
                curr = 0f;
                enterlock = false;
                islock = false;
            }
        }

        //バーの位置を更新
        if (bar != null)
        {
            bar.rectTransform.anchoredPosition = new Vector2(farstpos.x, farstpos.y + curr);
        }

        //バーがゴールに到達しているかどうか
        if (Ingoal() && !enterlock && Input.GetKeyUp(KeyCode.Return))
        {
            clear = true;
            this.gameObject.SetActive(false);
        }
    }



}