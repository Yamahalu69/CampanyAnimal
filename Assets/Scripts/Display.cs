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

    private Vector2 farstpos;
    private float curr;

    private void Start()
    {
        if(bar != null)
        {
            farstpos = bar.rectTransform.anchoredPosition;
            curr = 0f;
            bar.rectTransform.anchoredPosition = farstpos;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            curr += movesp * Time.deltaTime;

            if(curr > movedistance)
            {
                curr = 0f;
            }

            bar.rectTransform.anchoredPosition = new Vector2(farstpos.x, farstpos.y + curr);
        }
        else if(Input.GetKeyUp(KeyCode.Return))
        {
            curr = 0f;
            bar.rectTransform.anchoredPosition = farstpos;
        }
    }


}
