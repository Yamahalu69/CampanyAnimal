using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
using UnityEngine.Analytics;
using NUnit.Framework;

public class RegisterTask : MonoBehaviour
{
    [Header("単体起動用か")]
    public bool isDebug;

    public bool isPlaying;

    [Tooltip("KeyCodeのList")]
    List<KeyCode> arrowsList = new List<KeyCode>();

    //[Header("タスク完了に必要な矢印入力回数")]
    //public int arrowsAmount;

    [Header("矢印パターンリスト(ArrowList)")]
    public List<ArrowList> arrowLists;

    [Header("店員会話内容")]
    public List<string> clerkTalks;

    [Header("客会話内容")]
    public List<string> customerTalks;

    [Header("吹き出しオブジェクト")]
    public GameObject talkPrefab;

    //吹き出しオブジェクトリスト
    private List<GameObject> talkList = new List<GameObject>();

    [Header("店員吹き出し表示位置")]
    public Transform clerkTransform;

    [Header("客吹き出し表示位置")]
    public Transform customerTransform;

    [Header("吹き出し上昇量")]
    public float talkRise;

    [Header("吹き出し移動時間")]
    public float animationTime;

    private int counter = 0;

    [Header("矢印表示用キャンバス")]
    public GameObject canvas;

    [Header("矢印表示オブジェクト")]
    public GameObject arrowPrefab;

    [Header("矢印残り数表示オブジェクト")]
    public GameObject arrowRemainObject;

    [Header("矢印表示の位置")]
    public Transform[] transforms = new Transform[4];

    [Header("矢印残り数表示の位置")]
    public Transform remainTransform;

    [Header("矢印表示の大きさ(倍率)")]
    public float[] size = new float[4];

    private List<SwitchSprite> arrowSprites = new List<SwitchSprite>();

    private List<GameObject> instantObject = new List<GameObject>();

    private Text remainText;

    private int arrowRemain;

    [SerializeField] Player player;

    void Start()
    {
        if (isDebug) StartTask();
    }

    void Update()
    {
        if (!isPlaying) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InterruptTask();
        }

        // 入力判定
        if (Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.RightArrow))
        {
            //アニメーション中の場合アニメーションスキップ
            StopAnimation();

            if (Input.GetKeyDown(arrowsList[counter]))
            {
                //正しい入力

                //カウンター増加
                counter++;

                //吹き出しＧＯ生成
                CreateConversation();

                //アニメーション
                StartAnimation();

                //タスク完了判定
                if (counter == arrowsList.Count)
                {
                    //タスク完了
                    Debug.Log("タスク完了");
                    StopTask();
                    player.taskfinish = true;
                }

                //表示更新
                View();
            }
            else
            {
                //間違った入力
                //再スタート
                StopAnimation();
                StartTask();
            }
        }
    }

    private void StartAnimation()
    {
        foreach (GameObject talkGO in talkList)
        {
            Vector3 pos = new Vector3(0, talkRise, 0) + talkGO.transform.position;
            talkGO.transform.DOMove(pos, animationTime);
        }
    }

    private void StopAnimation()
    {
        if (talkList.Count == 0) return;
        foreach (GameObject talkGO in talkList)
        {
            talkGO.transform.DOComplete();
        }
    }

    public void StartTask()
    {
        //初期化処理
        isPlaying = true;
        counter = 0;

        //吹き出しオブジェクトがあるなら削除
        if (talkList.Count != 0)
        {
            foreach (GameObject go in talkList)
            {
                Destroy(go);
            }
            talkList.Clear();
        }

        //矢印パターンからKeyCodeを生成
        SetPatternArrow();

        //矢印表示用オブジェクト生成
        CreateArrowObject();

        //矢印残り数表示オブジェクト生成
        if (GameObject.Find("ArrowRemain(Clone)") == null)
        {
            GameObject remainObject = Instantiate(arrowRemainObject, remainTransform.position, Quaternion.identity);
            remainText = GameObject.Find("ArrowRemainText").GetComponent<Text>();
            remainObject.transform.SetParent(canvas.transform);
            remainObject.transform.position = remainTransform.position;
            instantObject.Add(remainObject);
        }

        //表示更新
        View();
    }

    void StopTask()
    {

        foreach (GameObject o in instantObject.ToList())
        {
            Destroy(o);
        }
        instantObject.Clear();
    }

    void CreateArrowObject()
    {
        if (arrowSprites.Count == 0)
        {
            //矢印表示用オブジェクト生成
            for (int i = 0; i < 4; i++)
            {
                //Vector3 pos = new(1510f - 450 * i, 200f, 0f);
                Vector3 pos = transforms[i].position;

                GameObject arrow = Instantiate(arrowPrefab, Vector2.zero, Quaternion.identity);
                arrowSprites.Add(arrow.GetComponent<SwitchSprite>());
                arrow.transform.SetParent(canvas.transform);
                arrow.transform.position = pos;
                arrow.transform.localScale *= size[i];
                instantObject.Add(arrow);
            }
        }
    }

    void View()
    {
        int loopCounter = 0;
        foreach (SwitchSprite s in arrowSprites)
        {
            if (counter + loopCounter < arrowsList.Count)
            {
                s.SwitchSpriteTo(arrowsList[counter + loopCounter]);
            }
            else
            {
                s.SwitchSpriteTo(KeyCode.Space);
            }
            loopCounter++;
        }
        arrowRemain = arrowsList.Count - counter;
        remainText.text = arrowRemain.ToString() + "こ";
    }

    void CreateConversation()
    {
        GameObject clarkTalkGO = Instantiate(talkPrefab, clerkTransform.position, Quaternion.identity);
        Text clarkTalkText = clarkTalkGO.GetComponentInChildren<Text>();
        clarkTalkGO.transform.SetParent(canvas.transform);
        clarkTalkText.text = clerkTalks[Random.Range(0, clerkTalks.Count - 1)];
        talkList.Add(clarkTalkGO);
        instantObject.Add(clarkTalkGO);

        GameObject customerTalkGO = Instantiate(talkPrefab, customerTransform.position, Quaternion.identity);
        Text customerTalkText = customerTalkGO.GetComponentInChildren<Text>();
        customerTalkGO.transform.SetParent(canvas.transform);
        customerTalkText.text = customerTalks[Random.Range(0, customerTalks.Count - 1)];
        talkList.Add(customerTalkGO);
        instantObject.Add(customerTalkGO);
    }

    void SetPatternArrow()
    {
        arrowsList.Clear();
        Random.InitState(System.DateTime.Now.Millisecond);
        List<Arrow> arrows = arrowLists[Random.Range(0, arrowLists.Count)].arrowList;
        
        foreach (Arrow arrow in arrows)
        {
            switch (arrow)
            {
                case Arrow.Up:
                    arrowsList.Add(KeyCode.UpArrow);
                    break;
                case Arrow.Down:
                    arrowsList.Add(KeyCode.DownArrow);
                    break;
                case Arrow.Right:
                    arrowsList.Add(KeyCode.RightArrow);
                    break;
                case Arrow.Left:
                    arrowsList.Add(KeyCode.LeftArrow);
                    break;
            }
        }
    }

    /*void SetRandamArrow()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        arrowsList.Clear();
        for (int i = 0; i < arrowsAmount; i++)
        {
            int r = Random.Range(1, 5);
            Debug.Log(r);
            switch (r)
            {
                case 1:
                    arrowsList.Add(KeyCode.UpArrow);
                    break;
                case 2:
                    arrowsList.Add(KeyCode.DownArrow);
                    break;
                case 3:
                    arrowsList.Add(KeyCode.RightArrow);
                    break;
                case 4:
                    arrowsList.Add(KeyCode.LeftArrow);
                    break;
            }
        }
    }*/

    void InterruptTask()
    {
        Debug.Log("ゲームを中断しました");
        StopTask();
        isPlaying = false;
    }
}
