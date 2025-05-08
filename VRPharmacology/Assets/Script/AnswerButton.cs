using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    public bool correctAnswer;
    public TMP_Text alternativ; // Knappens svar
    [SerializeField] private TMP_Text uiText; // Snakkebobblen til Mascot
    [SerializeField] private Animator animator; // "Mascots" animator
    [SerializeField] private Animator curtainAnimator; // Animerer gardinen animator

    [SerializeField] private GameObject gamelogic;
    private GameLogic gameController;
    private SoundManager sounds;

    // Følgene er tilbakemelding på rett og galt svar.
    private string correctAnswerText;
    private string wrongAnswerText;

    public void ClickMe(){
        gamelogic.GetComponent<MonoBehaviour>().StopAllCoroutines();
        if (correctAnswer){
            uiText.text = correctAnswerText;
            sounds.PlaySFX(sounds.correctAnswer);
            animator.Play("ThumbsUP", 2, 0f);
            gameController.incScore();
        } else {
            animator.Play("WrongAnswer", 2, 0f);
            sounds.PlaySFX(sounds.wrongAnswer);
            uiText.text = wrongAnswerText;
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
        gameController.startButtonVisibility();
        curtainAnimator.Play("Down", 0, 0f);
    }
    private void RightOrWrong() {
        var colors = GetComponent<Button> ().colors;
        if (correctAnswer) {
             // Stjålet denne koden fra nettet, veit ikke hva for noe svart magi dette er
             // NOTE: 06.05.25, husker ikke hvem jeg tokk det fra, men sannsynligvis Tim-C
             // https://discussions.unity.com/t/changing-the-color-of-a-ui-button-with-scripting/549785/5
            colors.disabledColor = Color.green;
            GetComponent<Button> ().colors = colors;
          } else {
            colors.disabledColor = Color.red;
            GetComponent<Button> ().colors = colors;
          }
    }
    public void buttonSetup(string text, bool correctAnswer, string correctText, string wrongText) {
        this.correctAnswer = correctAnswer;
        alternativ.text = text;
        correctAnswerText = correctText;
        wrongAnswerText = wrongText;
    }
        private void Awake() {
        sounds = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
        gameController = gamelogic.GetComponent<GameLogic>();
    }
}
