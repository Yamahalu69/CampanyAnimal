using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Clean : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private GameObject cleantask;
    [SerializeField,Header("ƒ^ƒXƒNŠ®—¹‚Ü‚Å‚Ì“ü—Í‰ñ”")] private int count;//“ü—Í‰ñ”

    private int keypress = 0;
    private bool isclean = false;

    private void Start()
    {
        if(bar != null)
           bar.fillAmount = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        if (bar == null || isclean) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            keypress++;
            bar.fillAmount = Mathf.Clamp01((float)keypress / count);
            if (keypress >= count)
            {
                isclean = true;
                StartCoroutine(Delay(3f));
            }
        }

    }
    

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (cleantask != null)
            Destroy(cleantask);
    }
}
