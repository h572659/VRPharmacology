using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    public bool correctAnswer;
    public TMP_Text alternativ; // Knappens svar
    [SerializeField] private TMP_Text uiText; // Snakkebobblen til Mascot
    [SerializeField] private Animator animator; // "Mascots" animator
    [SerializeField] private GameObject gamelogic;

    public void ClickMe(){
        gamelogic.GetComponent<MonoBehaviour>().StopAllCoroutines();
        animator.Play("Talk", 1, 0f);
        if (correctAnswer){
            uiText.text = "Correct!";
        } else {
            uiText.text = "Wrong!";
        }
        foreach (Transform child in transform.parent)
        {
            if (child.gameObject.activeSelf == false)  // Only activate if it's not active
            {
                child.gameObject.SetActive(true);
            }
            child.GetComponent<Button>().interactable = false;
            child.GetComponent<AnswerButton>().RightOrWrong();
        }
        gamelogic.GetComponent<GameLogic>().startButtonVisibility();
    }
    private void RightOrWrong() {
        var colors = GetComponent<Button> ().colors;
        if (correctAnswer) {
             //Stjålet denne koden fra nettet, veit ikke hva for noe svart magi dette er
            colors.disabledColor = Color.green;
            GetComponent<Button> ().colors = colors;
          } else {
            colors.disabledColor = Color.red;
            GetComponent<Button> ().colors = colors;
          }
    }
    public void buttonSetup(string text, bool correctAnswer) {
        this.correctAnswer = correctAnswer;
        alternativ.text = text;
    }
}
