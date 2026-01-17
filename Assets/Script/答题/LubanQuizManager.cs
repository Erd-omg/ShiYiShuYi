using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class LubanQuizManager : MonoBehaviour
{
    // TextMeshPro文本组件
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI feedbackText;
    public Button[] optionButtons;
    public Button nextButton;
    public Button prevButton;
    public GameObject successPanel;

    // 鲁班锁专用配置
    private string topicFile = "LuBanQuestions";
    private string topicName = "问答环节";

    private List<QuestionData> currentQuestions = new List<QuestionData>();
    private int currentQuestionIndex = 0;

    public Item AwardItem;

    void Start()
    {
        LoadQuestions();
        UpdateUI();

        // 添加按钮监听
        nextButton.onClick.AddListener(NextQuestion);
        prevButton.onClick.AddListener(PreviousQuestion);
        for (int i = 0; i < optionButtons.Length; i++)
        {
            int index = i;
            optionButtons[i].onClick.AddListener(() => OnOptionSelected(index));
        }
    }

    void LoadQuestions()
    {
        currentQuestions.Clear();

        TextAsset questionFile1 = Resources.Load<TextAsset>(topicFile);
        if (questionFile1 == null)
        {
            Debug.LogError("无法加载题目文件: " + topicFile);
            // 创建默认问题避免空列表
            CreateDefaultQuestion();
            return;
        }

        // 读取鲁班锁题目文件
        TextAsset questionFile = Resources.Load<TextAsset>(topicFile);
        if (questionFile == null)
        {
            Debug.LogError("无法加载题目文件: " + topicFile);
            return;
        }

        string[] lines = questionFile.text.Split('\n');
        QuestionData currentQuestion = null;

        foreach (string line in lines)
        {
            if (string.IsNullOrEmpty(line.Trim())) continue;

            if (line.StartsWith("题目:"))
            {
                if (currentQuestion != null)
                {
                    currentQuestions.Add(currentQuestion);
                }
                currentQuestion = new QuestionData();
                currentQuestion.question = line.Substring(3).Trim();
            }
            else if (line.StartsWith("选项A:"))
            {
                currentQuestion.options[0] = line.Substring(4).Trim();
            }
            else if (line.StartsWith("选项B:"))
            {
                currentQuestion.options[1] = line.Substring(4).Trim();
            }
            else if (line.StartsWith("选项C:"))
            {
                currentQuestion.options[2] = line.Substring(4).Trim();
            }
            else if (line.StartsWith("选项D:"))
            {
                currentQuestion.options[3] = line.Substring(4).Trim();
            }
            else if (line.StartsWith("正确答案:"))
            {
                string answer = line.Substring(5).Trim();
                switch (answer)
                {
                    case "A": currentQuestion.correctAnswerIndex = 0; break;
                    case "B": currentQuestion.correctAnswerIndex = 1; break;
                    case "C": currentQuestion.correctAnswerIndex = 2; break;
                    case "D": currentQuestion.correctAnswerIndex = 3; break;
                }
            }
        }

        if (currentQuestion != null)
        {
            currentQuestions.Add(currentQuestion);
        }

        // 设置标题
        titleText.text = topicName;

        if (currentQuestions.Count == 0)
        {
            Debug.LogWarning("题目文件为空，创建默认问题");
            CreateDefaultQuestion();
        }
    }

    void CreateDefaultQuestion()
    {
        QuestionData defaultQuestion = new QuestionData();
        defaultQuestion.question = "默认问题：请确保题目文件存在且格式正确";
        defaultQuestion.options[0] = "选项A";
        defaultQuestion.options[1] = "选项B";
        defaultQuestion.options[2] = "选项C";
        defaultQuestion.options[3] = "选项D";
        defaultQuestion.correctAnswerIndex = 0;
        currentQuestions.Add(defaultQuestion);
    }
    void UpdateUI()
    {
        // 添加保护性检查
        if (currentQuestions.Count == 0)
        {
            Debug.LogWarning("没有问题可显示");
            questionText.text = "暂无题目";
            return;
        }

        if (currentQuestionIndex < 0 || currentQuestionIndex >= currentQuestions.Count)
        {
            Debug.LogError($"当前问题索引 {currentQuestionIndex} 超出范围，重置为0");
            currentQuestionIndex = 0;
        }

        QuestionData currentQ = currentQuestions[currentQuestionIndex];
        questionText.text = currentQ.question;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = optionButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = currentQ.options[i];
            }

            // 重置按钮颜色
            ColorBlock colors = optionButtons[i].colors;
            colors.normalColor = Color.white;
            optionButtons[i].colors = colors;

            // 如果已回答正确，设置绿色
            if (currentQ.isAnswered && currentQ.isCorrect && i == currentQ.correctAnswerIndex)
            {
                colors.normalColor = Color.green;
                optionButtons[i].colors = colors;
            }
        }

        // 显示或隐藏反馈信息
        if (currentQ.isAnswered)
        {
            feedbackText.gameObject.SetActive(true);
            feedbackText.text = currentQ.isCorrect ? "恭喜你，回答正确" : "很遗憾，你并未通过考验";
            feedbackText.color = currentQ.isCorrect ? Color.green : Color.red;
        }
        else
        {
            feedbackText.gameObject.SetActive(false);
        }

        // 更新导航按钮状态
        prevButton.interactable = currentQuestionIndex > 0;
        nextButton.interactable = currentQuestionIndex < currentQuestions.Count - 1;

        // 检查是否全部答对
        CheckAllCorrect();
    }

    void OnOptionSelected(int optionIndex)
    {
        // 添加边界检查 - 修复 ArgumentOutOfRangeException
        if (currentQuestions.Count == 0 || currentQuestionIndex >= currentQuestions.Count)
        {
            Debug.LogError("问题列表为空或当前索引超出范围");
            return;
        }

        QuestionData currentQ = currentQuestions[currentQuestionIndex];

        // 添加选项索引检查
        if (optionIndex < 0 || optionIndex >= currentQ.options.Length)
        {
            Debug.LogError($"选项索引 {optionIndex} 超出范围 (0-{currentQ.options.Length - 1})");
            return;
        }

        if (currentQ.isAnswered) return;

        currentQ.isAnswered = true;
        if (optionIndex == currentQ.correctAnswerIndex)
        {
            currentQ.isCorrect = true;
            feedbackText.text = "恭喜你，回答正确";
            feedbackText.color = Color.green;
        }
        else
        {
            currentQ.isCorrect = false;
            feedbackText.text = "很遗憾，你并未通过考验";
            feedbackText.color = Color.red;

            // 答错后重置所有题目状态
            Invoke("ResetQuestions", 1.5f);
        }

        feedbackText.gameObject.SetActive(true);
        UpdateUI();
    }

    void ResetQuestions()
    {
        foreach (QuestionData question in currentQuestions)
        {
            question.isAnswered = false;
            question.isCorrect = false;
        }
        UpdateUI();
    }

    void NextQuestion()
    {
        if (currentQuestionIndex < currentQuestions.Count - 1)
        {
            currentQuestionIndex++;
            UpdateUI();
        }
    }

    void PreviousQuestion()
    {
        if (currentQuestionIndex > 0)
        {
            currentQuestionIndex--;
            UpdateUI();
        }
    }

    void CheckAllCorrect()
    {
        foreach (QuestionData question in currentQuestions)
        {
            if (!question.isCorrect) return;
        }

        // 全部答对，显示成功面板
        successPanel.SetActive(true);
        addItem();
    }

    public void RestartQuiz()
    {
        successPanel.SetActive(false);
        currentQuestionIndex = 0;
        LoadQuestions();
        UpdateUI();
    }

    public void addItem()
    {
        if (PersistenceManager.instance != null)
        {
            PersistenceManager.instance.AddItemToInventory(AwardItem);
        }
        else
        {
            Debug.LogError("PersistenceManager 实例未找到！");
        }
    }
}