using UnityEngine;

[System.Serializable]
public class QuizQuestion {
  public string question;
  public string[] answers;
  public int correctIndex;
  public bool man;
  public string wrongAnswerText;
  public string correctAnswerText;

}