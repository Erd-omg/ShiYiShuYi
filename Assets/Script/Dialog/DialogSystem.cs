using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header("UI组件")]
    public TMP_Text textLabel;
    public Image faceA;
    public Image faceB;
    public TMP_Text nameA;
    public TMP_Text nameB;
    public Image dialogBox;
    public Sprite boxLeft;
    public Sprite boxRight;
    public Image yhsj;

    [Header("文本文件")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;

    [Header("人物")]
    public Sprite A,B;

    [Header("杨辉三角")]
    public Sprite a, b, c;

    [Header("对话后")]
    public GameObject dialogPanel;
    public GameObject nextObj;
    public bool changeScene=false;
    public string SceneName;

    bool textFinished;//是否完成打字
    bool cancelTyping;//取消逐字输入

    List<string> textList = new List<string>();

    //改变文本框的位置
    Vector2 LabelA = new Vector2(170f,-11.5f);
    Vector2 LabelB = new Vector2(-170f, -11.5f);

    void Awake()
    {
        GetTextFromFile(textFile);//读取文件
        faceA.enabled = false;
        faceB.enabled = false;
        if(yhsj!=null)
            yhsj.enabled = false;

    }

    //一开始就直接输出第一行（不用点鼠标）
    private void OnEnable()
    {
        StartCoroutine(SetTextUI());
    }

    void Update()
    {
        //输出结束后关闭文本框（避免报错）
        if (Input.GetMouseButtonDown(0) && index == textList.Count)
        {
            gameObject.SetActive(false);

            if (changeScene)
                SceneManager.LoadScene(SceneName);

            else if (dialogPanel != null)
            {
                dialogPanel.SetActive(false);
                if(nextObj != null)
                    nextObj.SetActive(true);
            }

            index = 0;
            return;
        }

        //保证输出完上一行的内容（避免出现乱码）
        if (Input.GetMouseButtonDown(0))
        {
            if (textFinished && !cancelTyping)//如果上一行打字结束，以及这一行没有取消逐字输入
            {
                StartCoroutine(SetTextUI());//逐字输出这一行
            }
            else if (!textFinished)
            {
                cancelTyping = !cancelTyping;//只要按下鼠标左键，就能改变cancelTyping的bool值

            }
        }
    }

    //读取文件（每一行）
    void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        var lineData = file.text.Split('\n');

        foreach (var line in lineData)
        {
            textList.Add(line);
        }

    }

    //逐字输出
    IEnumerator SetTextUI()
    {
        textFinished = false;
        textLabel.text = "";//清空上一行的文字

        //识别到文本中的角色，切换头像
        switch (textList[index].Trim().ToString())
        {
            case "A":
                faceA.enabled = true;
                nameA.enabled = true;
                faceA.sprite = A;
                faceB.enabled = false;
                nameB.enabled = false;
                dialogBox.sprite = boxLeft;

                textLabel.rectTransform.anchoredPosition = LabelA;
                index++;
                break;
            case "B":
                faceB.enabled = true;
                nameB.enabled = true;
                faceB.sprite = B;
                faceA.enabled = false;
                nameA.enabled = false;
                dialogBox.sprite = boxRight;

                textLabel.rectTransform.anchoredPosition = LabelB;
                index++;
                break;

            //显示杨辉三角图片
            case ("A1"):
                faceA.enabled = true;
                nameA.enabled= true;
                faceA.sprite = A;
                faceB.enabled = false;
                nameB.enabled = false;
                dialogBox.sprite = boxLeft;

                yhsj.enabled = true;
                yhsj.sprite = a;

                textLabel.rectTransform.anchoredPosition = LabelA;
                index++;
                break;
            case "B2":
                faceB.enabled = true;
                nameB.enabled = true;
                faceB.sprite = B;
                faceA.enabled = false;
                nameA.enabled = false;
                dialogBox.sprite = boxRight;

                yhsj.enabled = true;
                yhsj.sprite = b;

                textLabel.rectTransform.anchoredPosition = LabelB;
                index++;
                break;
            case "B3":
                faceB.enabled = true;
                nameB.enabled= true;
                faceB.sprite = B;
                faceA.enabled = false;
                nameA.enabled = false;
                dialogBox.sprite = boxRight;

                yhsj.enabled = true;
                yhsj.sprite = c;

                textLabel.rectTransform.anchoredPosition = LabelB;
                index++;
                break;
            case ("A0"):
                faceA.enabled = true;
                nameA.enabled = true;
                faceA.sprite = A;
                faceB.enabled = false;
                nameB.enabled = false;
                dialogBox.sprite = boxLeft;

                yhsj.enabled = false;

                textLabel.rectTransform.anchoredPosition = LabelA;
                index++;
                break;
        }

        //检测是否取消逐字输入，以及获得文本每行的每一个字符
        int letter = 0;
        while (!cancelTyping && letter < textList[index].Length - 1)
        {
            textLabel.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);//textSpeed即每个字输出的速度
        }
        textLabel.text = textList[index];//若取消逐字输入，直接整行输出
        cancelTyping = false;

        textFinished = true;
        index++;
    }
}

    

