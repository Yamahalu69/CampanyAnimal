using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    [SerializeField] Image bar;
    [SerializeField] Image goal;
    [SerializeField] private float movedistance = 200f;//高さ
    [SerializeField] private float movesp = 2f;//上昇スピード
    [SerializeField] private float downsp = 2f;//加工スピード

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

    private void Update()
    {
        if (clear) return;
        //バー上昇
        if (!islock && !enterlock  && Input.GetKey(KeyCode.Return))
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
        if(!islock && !Input.GetKey(KeyCode.Return) && curr > 0f)
        {
            enterlock = true;
            islock = true;
        }

        //バーが下降中
        if(enterlock)
        {
            curr -=downsp * Time.deltaTime;
            if(curr <= 0f)
            {
                curr = 0f;
                enterlock = false;
                islock = false;
            }
        }

        //バーの位置を更新
        if(bar != null)
        {
            bar.rectTransform.anchoredPosition = new Vector2(farstpos.x, farstpos.y + curr);
        }

        bool ingoalrange = Ingoal();
        //バーがゴールに到達しているかどうか
        if (ingoalrange && Input.GetKeyUp(KeyCode.Return))
        {
            clear = true;
            this.gameObject.SetActive(false);
        }
    }

    private bool Ingoal()
    {
        float goaltop = goal.rectTransform.anchoredPosition.y;
        float goalbottom = goaltop - goal.rectTransform.rect.height;

        return curr + farstpos.y >= goalbottom && curr+farstpos.y <= goaltop;
    }

}