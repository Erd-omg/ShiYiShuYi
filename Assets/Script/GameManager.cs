using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

class MathObjectConfig
{
    public List<string> MathObjectList;
}

public class GameManager : MonoBehaviour
{
    GameObject _originCard;
    int row = 3;
    int col = 4;
    float _cardH = 3;
    float _spaceX = 0.5f;
    float _spaceY = 0.5f;
    float upOffset = 1f;
    public static GameManager Instance;
    List<Card> _cardList;
    Card _currentTarget;
    List<Card> _compareCardList;
    List<Card> _rotateCardList;
    public GameObject lastObj;
    public GameObject rawImage;
    public GameObject successPanel;
    public GameObject failPanel;
    public GameObject shadowPanel;
    public Button restart;

    // 新增：规则面板和背包面板
    public GameObject rulePanel;
    public GameObject backpackPanel;

    // 新增：打开面板的按钮
    public Button ruleButton;
    public Button backpackButton;

    // 新增：关闭面板的按钮
    public Button closeRuleButton;
    public Button closeBackpackButton;

    public float timeTarget;//倒计时总时间
    float timer = 0f;
    Text timeText;

    bool start = false;
    bool _gameover = false;
    bool isGameRunnig = false;

    // 新增：游戏暂停状态
    bool _isPaused = true; // 初始设置为true，游戏开始时暂停

    void Start()
    {
        Instance = this;

        GameObject text = GameObject.Find("time");
        timeText = text.GetComponent<Text>();

        _originCard = Resources.Load<GameObject>("Card");

        _cardList = new List<Card>();
        _compareCardList = new List<Card>();
        _rotateCardList = new List<Card>();

        Init();
        restart.onClick.AddListener(RestartGame);

        // 新增：绑定面板按钮事件
        if (ruleButton != null)
            ruleButton.onClick.AddListener(OpenRulePanel);

        if (backpackButton != null)
            backpackButton.onClick.AddListener(OpenBackpackPanel);

        if (closeRuleButton != null)
            closeRuleButton.onClick.AddListener(CloseRulePanel);

        if (closeBackpackButton != null)
            closeBackpackButton.onClick.AddListener(CloseBackpackPanel);

        // 新增：游戏开始时显示规则面板
        if (rulePanel != null)
            rulePanel.SetActive(true);

        if (backpackPanel != null)
            backpackPanel.SetActive(false);

        // 新增：游戏开始时处于暂停状态
        _isPaused = true;
    }

    void Update()
    {
        // 新增：如果游戏暂停，不执行更新逻辑
        if (_isPaused) return;

        MouseDetect();
        MouseInput();

        if (!lastObj.activeSelf) start = true;
        if (start) timer = timer + Time.deltaTime;
        if (timer >= 1f && timeTarget > 0f)
        {
            timeTarget -= 1f;
            timeText.text = timeTarget.ToString();
            timer = 0f;
            if (_gameover)
            {
                timeTarget = 0f;
                successPanel.SetActive(true);
                shadowPanel.SetActive(true);
                _isPaused = true; // 游戏结束时暂停
            }
        }

        else if (timeTarget <= 0f && !_gameover)
        {
            failPanel.SetActive(true);
            shadowPanel.SetActive(true);
            _isPaused = true; // 游戏结束时暂停
        }
    }

    //初始化卡片和时间
    private void Init()
    {
        //初始化倒计时
        timer = 0f;
        timeTarget = 60f;
        timeText.text = timeTarget.ToString();

        string config = Resources.Load<TextAsset>("config").text;
        var maths = JsonUtility.FromJson<MathObjectConfig>(config);

        List<string> randomList = new List<string>();
        List<string> originList = new List<string>();

        //生成随机列表(洗牌）
        originList.AddRange(maths.MathObjectList);
        int count = originList.Count;
        for (int i = 0; i < count; i++)
        {
            int random = Random.Range(0, originList.Count);
            randomList.Add(originList[random]);
            originList.RemoveAt(random);
        }

        originList.AddRange(maths.MathObjectList);
        for (int i = 0; i < count; i++)
        {
            int random = Random.Range(0, originList.Count);
            randomList.Add(originList[random]);
            originList.RemoveAt(random);
        }

        //计算第一张卡片在游戏世界中的位置
        Vector3 offset = Vector3.down * ((row - 1) / 2 * (_cardH + _spaceY) - upOffset) + Vector3.left * (col - 1) / 2 * (_cardH + _spaceX);

        //实例化卡片并布局
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                GameObject cloneCard = Instantiate(_originCard);
                var card = cloneCard.GetComponent<Card>();
                //初始化卡片
                card.Initial(randomList[i * col + j]);
                _cardList.Add(card);
                //设置卡片的位置
                cloneCard.transform.position = transform.position + offset + Vector3.up * i * (_cardH + _spaceY) + Vector3.right * j * (_cardH + _spaceX);
                cloneCard.transform.SetParent(transform);
            }
        }
    }

    //卡片旋转
    private void MouseInput()
    {
        // 新增：如果游戏暂停，不处理鼠标输入
        if (_isPaused) return;

        if (Input.GetMouseButtonDown(0) && !lastObj.activeSelf)
        {
            if (_currentTarget != null && _rotateCardList.Count < 2 && !_rotateCardList.Contains(_currentTarget))
            {
                _currentTarget.Rotate();
            }
        }
    }

    //存储可以旋转的卡片
    public void AddCard(Card card)
    {
        // 新增：如果游戏暂停，不添加卡片
        if (_isPaused) return;

        _rotateCardList.Add(card);
    }

    //比较两张卡片
    public void CompareCards(Card card)
    {
        // 新增：如果游戏暂停，不比较卡片
        if (_isPaused) return;

        _compareCardList.Add(card);
        if (_compareCardList.Count == 2)
        {
            if (_compareCardList[0].MathObject == _compareCardList[1].MathObject)
            {
                _compareCardList[0].SwitchState(StateEnum.已配对);
                _compareCardList[1].SwitchState(StateEnum.已配对);

                //销毁卡片
                DestroyCard(_compareCardList[0]);
                DestroyCard(_compareCardList[1]);

                Clear();
                if (IsVictory())
                {
                    Victory();
                }
            }
            else
            {
                _compareCardList[0].Rotate(false);
                _compareCardList[1].Rotate(false);
            }
        }
    }

    //销毁已配对的卡片
    private void DestroyCard(Card card)
    {
        _cardList.Remove(card);
        Destroy(card.gameObject);
    }

    private void Victory()
    {
        _gameover = true;
    }

    //清除列表中的卡片
    public void Clear()
    {
        _compareCardList.Clear();
        _rotateCardList.Clear();
    }

    //检测鼠标射线是否指向卡片
    private void MouseDetect()
    {
        // 新增：如果游戏暂停，不检测鼠标
        if (_isPaused) return;

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(mouseRay, out hitInfo))
        {
            Card card = hitInfo.transform.GetComponent<Card>();
            if (card != null)
            {
                _currentTarget = card;
            }
        }
        else if (_currentTarget != null)
        {
            _currentTarget = null;
        }
    }

    //游戏胜利检测
    bool IsVictory()
    {
        bool isVictory = true;
        for (int i = 0; i < _cardList.Count; i++)
        {
            if (_cardList[i].CurrentState == StateEnum.未配对)
            {
                isVictory = false;
            }
        }
        return isVictory;
    }

    // 重置游戏
    public void RestartGame()
    {
        // 重置游戏结束标志  
        _gameover = false;

        // 隐藏成功和失败面板  
        successPanel.SetActive(false);
        failPanel.SetActive(false);
        shadowPanel.SetActive(false);

        // 新增：确保规则和背包面板也被关闭
        if (rulePanel != null)
            rulePanel.SetActive(false);

        if (backpackPanel != null)
            backpackPanel.SetActive(false);

        // 恢复游戏状态（如果之前暂停了）
        _isPaused = false;

        //清除卡片
        foreach (var card in _cardList)
        {
            if (card.gameObject != null)
                Destroy(card.gameObject);
        }
        Clear();
        _cardList.Clear();

        Init();

        // 新增：重新开始游戏时显示规则面板
        if (rulePanel != null)
            rulePanel.SetActive(true);

        _isPaused = true; // 重新开始游戏时暂停，等待玩家关闭规则面板
    }

    // 新增：打开规则面板
    private void OpenRulePanel()
    {
        rulePanel.SetActive(true);
        _isPaused = true;
    }

    // 新增：关闭规则面板 - 开始游戏
    private void CloseRulePanel()
    {
        rulePanel.SetActive(false);
        _isPaused = false;
        start = true; // 确保游戏开始
    }

    // 新增：打开背包面板
    private void OpenBackpackPanel()
    {
        backpackPanel.SetActive(true);
        _isPaused = true;
    }

    // 新增：关闭背包面板
    private void CloseBackpackPanel()
    {
        backpackPanel.SetActive(false);
        _isPaused = false;
    }
}