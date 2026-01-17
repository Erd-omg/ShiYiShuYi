using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public GameObject dialog;
    public GameObject rawImage;

    public float timeTarget;//倒计时总时间
    float timer=0f;

    Text timeText;

    void Start()
    {
        GameObject text = GameObject.Find("time");
        timeText=text.GetComponent<Text>();
    }

    void Update()
    {
        if(!dialog.activeSelf)
            timer = timer + Time.deltaTime;
        if (timer >= 1f && timeTarget> 0f)
        {
            timeTarget-=1f;
            timeText.text = timeTarget.ToString();
            timer = 0f;
        }
    }

}
