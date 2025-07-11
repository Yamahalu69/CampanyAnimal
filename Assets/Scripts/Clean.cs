using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Clean : MonoBehaviour
{
    [SerializeField] private Player prayer;
    [SerializeField] private float fallspeed;
    [SerializeField] private Slider bar;
    [SerializeField] private GameObject cleantask;
    [SerializeField] private Text Enter;
    [SerializeField, Header("Å‘å“ü—Í‰ñ”")] private int maxcount;
     private int count;//Œ»Ý‚Ì“ü—Í‰ñ”

    private bool hascompleted = false;

    [SerializeField, Header("‘€ì‰Â”\‚Ü‚Å‚Ì’x‰„•b”")] private float delay = 0.5f;

    private float timer = 0f;
    private bool inputenabled = false;
    private void Start()
    {
        Enter.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        if (bar != null)
        {
            bar.value = 0f;
        }
        if ((cleantask !=null))
        {
            cleantask.SetActive(true);
        }
        timer = 0f;
        inputenabled = false;
    }

    private void Update()
    {
        if (hascompleted) return;

        if(!inputenabled)
        {
            timer += Time.deltaTime;
            if(timer>=delay)
            {
                inputenabled = true;
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            count++;
            Enter.color = new Color(255f, 0f, 0f, 0.5f);

            float progress = (float)count / maxcount;
            bar.value = Mathf.Clamp01(progress);

            if (count >= maxcount)
            {
                if (cleantask != null)
                {
                    cleantask.SetActive(false);
                    hascompleted = true;
                    prayer.CompleateTask();
                    prayer.csencer = false;
                    prayer.pl = true;
                }
            }
        }
       
        if(Input.GetKeyUp(KeyCode.Return))
        {
            Enter.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        }

    }   
}
