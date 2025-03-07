using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    [SerializeField] private bool correctAnswer;
    [SerializeField] private TMP_Text uiText; 
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
            child.GetComponent<AnswerButton>().RightOrWrong();
        }
    }
    private void RightOrWrong() {
        var colors = GetComponent<Button> ().colors;
        if (correctAnswer) {
             //Stjålet denne koden fra nettet, veit ikke hva for noe svart magi dette er
            colors.normalColor = Color.green;
            GetComponent<Button> ().colors = colors;
          } else {
            colors.normalColor = Color.red;
            GetComponent<Button> ().colors = colors;
          }
    }
}
