using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class showAwards : MonoBehaviour
{
    public GameObject awardsName;
    public GameObject awards;
    public GameObject successLogo;
    public GameObject successPanel;
    public GameObject button;

    public string textToPrint;
    private float animatespeed=2;
    private float printSpeed=0.2f;
    private int index=0;

    bool isAnimating = false;
   
    bool isShowed=false;

    Text text;
    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        if (successPanel.activeSelf&&!isShowed)
        {
            StartCoroutine(waitForSeconds());
            isShowed = true;
        }
    }

    IEnumerator waitToAwards()
    {
        if (isAnimating) yield break;
        isAnimating = true;

        successLogo.SetActive(false);
        awards.SetActive(true);

        float elapsedTime = 0;
        Vector3 startScale =awards.transform.localScale;
        Vector3 targetScale = new Vector3(1f, 1f, 1f);

        while (elapsedTime < animatespeed)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / animatespeed;

            //平滑过渡到目标大小
            awards.transform.localScale = Vector3.Lerp(startScale, targetScale, progress);

            //等待下一帧
            yield return null;
        }

        transform.localScale = targetScale;

        yield return new WaitForSeconds(1f);
        awardsName.SetActive(false);
        button.SetActive(true);

        isAnimating = false;
        }

    IEnumerator print() 
    {
            while (index < textToPrint.Length)
            {
                if (index < textToPrint.Length - 1)
                    text.text += textToPrint[index];

                yield return new WaitForSeconds(printSpeed);
                
                index++;
            }

            text.text += textToPrint[textToPrint.Length - 1];

    }
    IEnumerator waitForSeconds()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(waitToAwards());
        StartCoroutine(print());
    }

}
