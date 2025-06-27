using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GageManager : MonoBehaviour
{
    public Slider slider;　//スライダーの参照
    public float duration = 60f; //ゴールまでの時間(秒)

    private float timeEleapsed = 0f; //経過時間

    void Start()
    { 

    }

    void Update()
    {
        //時間経過するごとにスライダーの値を更新
        if (timeEleapsed < duration)
        {
            timeEleapsed += Time.deltaTime; //経過時間を更新
            slider.value = Mathf.Lerp
                (0f,1f,timeEleapsed / duration); //スライダーの値を更新(0～1に補間)
        }
    }
}
