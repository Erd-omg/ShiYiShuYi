using System;

[Serializable]
public class QuestionData
{
    public string question;
    public string[] options = new string[4];
    public int correctAnswerIndex;
    public bool isAnswered;
    public bool isCorrect;
}
