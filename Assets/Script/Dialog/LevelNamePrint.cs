using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelNamePrint : MonoBehaviour
{
    string str;
    Text textContent;
    int i = 0;//¥Ú”°ÀŸ∂»
    int index = 0;
    string str1 = "";
    bool ison = false;

    public GameObject canvas;
    public GameObject tipPanel;

    public int speed;

    private bool isPrinted=false;

    private void Start()
    {
        textContent = GetComponent<Text>();
        str = textContent.text;
        textContent.text = "";
        i = speed;
    }

    private void Update()
    {
        if (tipPanel != null && !tipPanel.activeSelf && !isPrinted)
        {
            ison = true;
            isPrinted = true;
        }
        else if(tipPanel== null && !isPrinted)
        {
            ison = true;
            isPrinted = true;
        }

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
                i = speed;
            }
        }
    }

    IEnumerator waitFourSeconds()
    {
        yield return new WaitForSeconds(1.2f);
        canvas.SetActive(false);
    }
}
