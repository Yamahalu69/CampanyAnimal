using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Clean : MonoBehaviour
{
    [SerializeField] private Slider bar;
    [SerializeField] private GameObject cleantask;
    [SerializeField, Header("Å‘å“ü—Í‰ñ”")] private int maxcount;
     private int count;//Œ»Ý‚Ì“ü—Í‰ñ”

    private bool hascompleted = false;

    private void Start()
    {
        if(bar != null)
        {
            bar.value = 0f;
        }
        if ((cleantask !=null))
        {
            cleantask.SetActive(true);
        }
    }

    private void Update()
    {
        if (hascompleted) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            count++;

            float progress = (float)count / maxcount;
            bar.value = Mathf.Clamp01(progress);

            if (count >= maxcount)
            {
                if (cleantask != null)
                {
                    cleantask.SetActive(false);
                    hascompleted = true;
                }
            }
        }

    }   
}
