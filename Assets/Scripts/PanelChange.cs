using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PanelChange : MonoBehaviour
{
    [SerializeField] private GameObject Operation1;
    [SerializeField] private GameObject Operation2;
    [SerializeField] private GameObject Operation3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Operation1.SetActive(true);
        Operation2.SetActive(false);
        Operation3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //âÊñ ÇêiÇﬂÇÈ
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(Operation1.activeSelf)
            {
                Operation1.SetActive(false);
                Operation2.SetActive(true);
            }
            else if (Operation2.activeSelf)
            {
                Operation2.SetActive(false);
                Operation3.SetActive(true);
            }
            
        }

        if (Operation3.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                AudioManager.instance.PlayingBGM();
                SceneManager.LoadScene("MainGameScene");
            }
        }


        //âÊñ ÇñﬂÇ∑
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Operation1.activeSelf)
            {
                SceneManager.LoadScene("TitleScene");
            }
            else if (Operation2.activeSelf)
            {
                Operation2.SetActive(false);
                Operation1.SetActive(true);
            }
            else if (Operation3.activeSelf)
            {
                Operation3.SetActive(false);
                Operation2.SetActive(true);
            }
        }
    }
}
