using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SunZiQuizManager : MonoBehaviour
{
    // TextMeshPro文本组件
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI feedbackText;
    public Button[] optionButtons;
    public Button nextButton;
    public Button prevButton;
    public GameObject successPanel;

    // 孙子算经专用配置
    private string topicFile = "SunZiQuestions";
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

        // 读取孙子算经题目文件
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
    }

    void UpdateUI()
    {
        if (currentQuestions.Count == 0) return;

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
        QuestionData currentQ = currentQuestions[currentQuestionIndex];

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
        Debug.Log("Next button clicked"); // 添加调试信息

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