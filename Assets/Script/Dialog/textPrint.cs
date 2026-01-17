using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class textPrint : MonoBehaviour
{
    string str;
    TMP_Text textContent;
    int i = 0;//¥Ú”°ÀŸ∂»
    int index = 0;
    string str1 = "";
    bool ison = false;

    public GameObject canvas;
    public GameObject rawImage;
    public GameObject dialog;


    private void Start()
    {
        textContent = GetComponent<TMP_Text>();
        str = textContent.text;
        textContent.text = "";
        i = 20;
    }

    private void Update()
    {
        if (dialog != null && rawImage != null)
        {
            if (!dialog.activeSelf && !rawImage.activeSelf)
                ison = true;
        }
        else if(dialog==null&&rawImage!=null)
        {
            if(!rawImage.activeSelf)
                ison=true;
        }
        else if(dialog==null&&rawImage==null) ison = true;

        if (ison)
        {
            i -= 1;
            if (i <= 0)
            {
                if (index >= str.Length)
                {
                    ison = false;
                    StartCoroutine(waitFourSeconds());
                    return;
                }
                str1 = str1 + str[index].ToString();
                textContent.text = str1;
                index += 1;
                i = 20;
            }
        }
    }

    IEnumerator waitFourSeconds()
    {
        yield return new WaitForSeconds(0.8f);
        canvas.SetActive(false);
    }
}
