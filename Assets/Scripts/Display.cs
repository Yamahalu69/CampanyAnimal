using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    [SerializeField] Image bar;
    [SerializeField] private float movedistance = 200f;
    [SerializeField] private float movesp = 2f;
    [SerializeField] private float downsp = 2f;

    private Vector2 farstpos;
    private float curr;
    private bool enterlock = false;

    private void Start()
    {
        if(bar != null)
        {
            farstpos = bar.rectTransform.anchoredPosition;
            bar.rectTransform.anchoredPosition = farstpos;
        }
    }

    private void Update()
    {
        KeyCode keyCode = KeyCode.Return;
        if(enterlock == false)
        {
            if(Input.GetKey(keyCode))
            {
                curr += movesp * Time.deltaTime;
                if(curr>=movedistance)
                {
                    curr = movedistance;
                    enterlock = true;
                }
            } 
        }
        else if(enterlock == true)
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                enterlock = true;
            }
        }


        if (!Input.GetKey(KeyCode.Return) && enterlock)
        {
            curr -= downsp * Time.deltaTime;
            if (curr <= 0f)
            {
                curr = 0f;
                enterlock = false;
            }
        }
            bar.rectTransform.anchoredPosition = new Vector2(farstpos.x, farstpos.y + curr);  
    }


}
