using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberGame : MonoBehaviour
{
  
    // Entities and their states / Model
    private static int count;  //用于记录玩家输入的总值
    private static int chance; //用于记录玩家能输入几次
    private static int total;  //用于记录目标值
    private static int time; //用于记录玩家输入了几次
    private int[,] KeyBoard = new int[3, 3]; //用于判断按钮是否被按过
    private int[,] NumberBoard = new int[3, 3]; //用于记录每个按钮对应的数字
    private Rect window0 = new Rect(260, 50, 200, 180); //提示窗口坐标信息
    private bool showWindow = false;  //用于显示提示窗口

    public Texture2D p1;
    public Texture2D p2;
    public Texture2D p3;
    public Texture2D p4;
    public Texture2D p5;
    public Texture2D p6;
    public Texture2D p7;
    public Texture2D p8;
    public Texture2D p9;

    // System Handlers
    void Start()
    {
        Init();
    }

    // View to render entities / models
    void OnGUI()
    {
        //GUI.Box(new Rect(210, 25, 300, 300), "");
        if (GUI.Button(new Rect(310, 270, 100, 30), "Restart"))
        {
            Init();
        }
        string total_content = "您的目标值为:" + total.ToString();
        GUI.Label(new Rect(250, 26, 100, 30), total_content);
        string chance_content = "您能输入的次数为:" + chance.ToString();
        GUI.Label(new Rect(355, 26, 120, 30), chance_content);
        if (showWindow)
        {
            GUI.Window(0, window0, oneWindow, "warnning");
        }
        if (time < chance)
        {
            if (GUI.Button(new Rect(255 + 0 * 70, 50 + 0 * 70, 70, 70), p1))
            {
                if (KeyBoard[0, 0] == 0)
                {
                    Press(0, 0);
                }
                else
                {
                    showWindow = true;
                }
            }
            if (GUI.Button(new Rect(255 + 1 * 70, 50 + 0 * 70, 70, 70), p2))
            {
                if (KeyBoard[0, 1] == 0)
                {
                    Press(0, 1);
                }
                else
                {
                    showWindow = true;
                }
            }
            if (GUI.Button(new Rect(255 + 2 * 70, 50 + 0 * 70, 70, 70), p3))
            {
                if (KeyBoard[0, 2] == 0)
                {
                    Press(0, 2);
                }
                else
                {
                    showWindow = true;
                }
            }
            if (GUI.Button(new Rect(255 + 0 * 70, 50 + 1 * 70, 70, 70), p4))
            {
                if (KeyBoard[1, 0] == 0)
                {
                    Press(1, 0);
                }
                else
                {
                    showWindow = true;
                }
            }
            if (GUI.Button(new Rect(255 + 1 * 70, 50 + 1 * 70, 70, 70), p5))
            {
                if (KeyBoard[1, 1] == 0)
                {
                    Press(1, 1);
                }
                else
                {
                    showWindow = true;
                }
            }
            if (GUI.Button(new Rect(255 + 2 * 70, 50 + 1 * 70, 70, 70), p6))
            {
                if (KeyBoard[1, 2] == 0)
                {
                    Press(1, 2);
                }
                else
                {
                    showWindow = true;
                }
            }
            if (GUI.Button(new Rect(255 + 0 * 70, 50 + 2 * 70, 70, 70), p7))
            {
                if (KeyBoard[2, 0] == 0)
                {
                    Press(2, 0);
                }
                else
                {
                    showWindow = true;
                }
            }
            if (GUI.Button(new Rect(255 + 1 * 70, 50 + 2 * 70, 70, 70), p8))
            {
                if (KeyBoard[2, 1] == 0)
                {
                    Press(2, 1);
                }
                else
                {
                    showWindow = true;
                }
            }
            if (GUI.Button(new Rect(255 + 2 * 70, 50 + 2 * 70, 70, 70), p9))
            {
                if (KeyBoard[2, 2] == 0)
                {
                    Press(2, 2);
                }
                else
                {
                    showWindow = true;
                }
            }

        }
        else if (time == chance)
        {
            if (GameOver() == true)
            {
                GUI.Box(new Rect(260, 50, 200, 200), "\n\n\n\n\nCongratulations!\n you has won.");
            }
            if (GameOver() == false)
            {
                GUI.Box(new Rect(260, 50, 200, 200), "\n\n\n\n\nYou loss!\n please try again.");
            }
        }
    }
    void oneWindow(int windowID)
    {
        GUI.Box(new Rect(10, 50, 180, 30), "同一个数字按钮不能多次按下");
        if (GUI.Button(new Rect(15, 100, 150, 50), "关闭"))
        {
            showWindow = false;
        }
    }

    // Components /controls
    void Init()
    {
        count = 0;
        time = 0;
        chance = Random.Range(1, 10);
        //Debug.Log("随机生成的chance: " + chance);
        int minTotal = 0;
        int maxTotal = 0;
        for (int i = 1; i <= chance; i++)
        {
            minTotal += i;
        }

        for (int i = 9; i >= 10 - chance; i--)
        {
            maxTotal += i;
        }
        total = Random.Range(minTotal, maxTotal + 1);
        //Debug.Log("随机生成的total: " + maxTotal);
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                KeyBoard[i, j] = 0;
            }
        }
        int temp = 1;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                NumberBoard[i, j] = temp;
                temp++;
            }
        }

    }


    void Press(int i, int j)
    {
        KeyBoard[i, j] = 1;
        count += NumberBoard[i, j];
        time++;
    }

    bool GameOver()
    {
        if (count != total)
        {
            return false;
        }
        return true;
    }
}

