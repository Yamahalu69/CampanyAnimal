using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tskUi : MonoBehaviour
{
    public GameObject PanelUIobj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PanelUIobj.SetActive(false);
    }
}
