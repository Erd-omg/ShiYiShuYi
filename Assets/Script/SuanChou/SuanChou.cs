using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuanChou : MonoBehaviour
{

    private static int count;  //用于记录玩家输入的总值
    private static int chance; //用于记录玩家能输入几次
    private static int total;  //用于记录目标值
    private static int time; //用于记录玩家输入了几次
    private static int num;

    private int[,] KeyBoard = new int[3, 3]; //用于判断按钮是否被按过
    private int[,] NumberBoard = new int[3, 3]; //用于记录每个按钮对应的数字
    public int success = 0;//用于记录成功的次数
    public bool LevelSuccessed = false;//用于判断本次成功是否已被记录
    public int LevelBool = 0;//用于记录单局状态：0表示null（初始状态），1表示本局成功，-1表示本局失败
    //private bool isPanelVisible = false; // 记录面板是否可见  //

    public Text targetText;//显示玩家应输入的数值对应文本框
    public Text timeText;//显示玩家可输入次数的文本框
    public Text countText;//显示玩家已输入数值
    //public Text clickText;//显示玩家已经输入的次数 
    public Button restart;//重新开始一局游戏
    public Button restartsuccess;//重新开始一局游戏
    public Button restartfail;//重新开始一局游戏
    public Button[] numberButtons;//获取数字使用
    public GameObject successPanel;
    public GameObject warningPanel;
    public GameObject shadowPanel;
    public GameObject background;
    public GameObject LevelsuccessPanel;
    public GameObject LevelfailPanel;

    public static SuanChou instance;


    void Start()
    {
        Init();
        AddButtonListeners();
        restart.onClick.AddListener(Init);//点击按钮使游戏重开
        restartsuccess.onClick.AddListener(Init);//点击按钮使游戏重开
        restartfail.onClick.AddListener(Init);//点击按钮使游戏重开

        LevelsuccessPanel.SetActive(false);
        LevelfailPanel.SetActive(false);



        instance = this;
    }

    void Update()
    {
        num = chance - time;

        string total_content = "您的目标值为" + total.ToString();
        targetText.text = total_content;
        string chance_content = "您能输入的次数为" + num.ToString();
        timeText.text = chance_content;
        string count_content = "您已输入的总值为" + count.ToString();
        countText.text = count_content;
        

        GameOver();

        if (LevelBool==1)
        {
            if (success < 3)
            {
                if (!LevelSuccessed)
                    success++;
                if (success < 3)//保证第三局成功时不会弹出该panel
                {
                    LevelsuccessPanel.SetActive(true);
                    shadowPanel.SetActive(true);
                    background.SetActive(true);
                }
                LevelSuccessed = true;//保证不会重复计数
            }
            else if (success == 3)
            {
                successPanel.SetActive(true);
                shadowPanel.SetActive(true);
                background.SetActive(true);
            }
            LevelBool = 0;//每局结束后将状态（成功或失败）清空
        }
        else if(LevelBool==-1) 
        {
            LevelfailPanel.SetActive(true);
            shadowPanel.SetActive(true);
            background.SetActive(true);
            LevelBool = 0;//每局结束后将状态（成功或失败）清空
        }

        if (warningPanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            warningPanel.SetActive(false);
            shadowPanel.SetActive(false);
        }

    }

    void Init()
    {
        count = 0;
        time = 0;
        chance = Random.Range(1, 10);
        total = CalculateRandomTotal(chance);
        ResetKeyboard();
        PopulateNumberBoard();
    }

    int CalculateRandomTotal(int chance)
    {
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
        return Random.Range(minTotal, maxTotal + 1);
    }

    void ResetKeyboard()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                KeyBoard[i, j] = 0;
            }
        }
    }

    void PopulateNumberBoard()
    {
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
        if (KeyBoard[i, j] == 0) // 确保按钮只被按一次  
        {
            KeyBoard[i, j] = 1;
            count += NumberBoard[i, j];
            time++;
        }
        else
        {
            warningPanel.SetActive(true);
            shadowPanel.SetActive(true);
        }
    }

    void GameOver()
    {
        if (time == chance)
        {
            if (count == total)
                LevelBool = 1;
            else
                LevelBool = -1;
        }
    }

    void AddButtonListeners()
    {
        for (int i = 0; i < numberButtons.Length; i++)
        {
            int buttonIndex = i;
            int row = buttonIndex / 3;
            int col = buttonIndex % 3;

            numberButtons[i].onClick.AddListener(() => Press(row, col));
        }
    }
}