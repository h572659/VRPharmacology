using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class GameLogic : MonoBehaviour {
    [SerializeField] private TMP_Text uiText; // Heter UI, men burde heite snakkebobble tekst ellerno.
    [SerializeField] private float typeSpeed = 0.02f; // Ser litt kulere ut om ikke alt skrives ut med en gang, dette bestemmer farten
    [SerializeField] private float ButtonSpeed = 0.02f; // Tiden det tar mellom kver knapp blir synlig, igjen fordi dette ser kullere ut
    [SerializeField] private Animator animator; // "Mascots" animator
    [SerializeField] private Animator curtainAnimator; // Animerer gardinen animator
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject OptionsButtons; // Valg alternativene
    [SerializeField] private GameObject endScreen;
    [SerializeField] private GameObject snakkebobble;
    [SerializeField] private TMP_Text ScoreText;

    
    // Spørsmålene 
    private Stack<QuizQuestion> questionStack = new Stack<QuizQuestion>();
    // Spørsmålet
    private QuizQuestion question;
    // telle var for indexer
    private int index = 0;
    // Teksten som går inn i snakkebobblen
    private string fullText;
    // Variabel som blir brukt for skrive effekten i snakkebobblen. 
    private string currentText = "";
    private Coroutine typingCoroutine;
    // "Cacher" knappenes informasjon / script.
    private List<AnswerButton> buttonsLogic;
    // Lyd kontroller.
    private SoundManager sounds;
    private Patient patientModelController;
    private bool gameRunning = false;
    private int score = 0;
    private int antallSporsmal;

    private void Awake() {
        sounds = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
        patientModelController = GameObject.FindGameObjectWithTag("Patient").GetComponent<Patient>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        sounds.PlayMusic(sounds.before);
        // Henter ut Monoscriptene til knappene.
        buttonsLogic = new List<AnswerButton>();
        foreach (Transform child in OptionsButtons.transform) {
            buttonsLogic.Add(child.GetComponent<AnswerButton>());
        }
        LoadQuestionsFromJson();
        fullText = "Welcome! Please press play to start.";
        animator.Play("Talk", 1, 0f);
        typingCoroutine = StartCoroutine(ShowText());
    }

    // Gjør at teksten skrives ut litt etter litt
    IEnumerator ShowText() {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            uiText.text = currentText;
            yield return new WaitForSeconds(typeSpeed);
        }
            typingCoroutine = null;
    }

    public void Click(){ // Kjører når start knappen er presset
        if (!gameRunning){
            gameRunning = true;
            sounds.StopMusic();
            sounds.PlayMusic(sounds.start);
        }
        startButtonVisibility();
        // Rydder opp knapper
        foreach (Transform child in OptionsButtons.transform){
            child.gameObject.SetActive(false); // Sørger for at de ikke vises på skjerm
            child.GetComponent<Button>().interactable = true; // Sørger for at når de vises at de kan klikkes på
            AnswerButton button = child.GetComponent<AnswerButton>(); // MonoScriptet som bestemmer mye av atferden
            button.correctAnswer = false; // Svar er falsk ved default
        }
        // Stopper skrivingen tidlig om spiller trykker på start knappen.
        if (typingCoroutine != null){
            StopCoroutine(typingCoroutine);
            uiText.text = fullText;
        }
        questionLogic();
    }

    public void startButtonVisibility() { // Slår av og på "start"-knappen
        startButton.SetActive(!startButton.activeSelf);
    }

    // Denne metoden henter spørsmål fra stokken, og setter opp spørsmål og svar.
    private void questionLogic() {
        if (questionStack.Count != 0){
        question = questionStack.Pop(); // Henter spørsmål fra toppen av stacken TODO: Sjekk om den er tom.
        if (question.man){
            patientModelController.PatientMan();
        } else {
            patientModelController.PatientWoman();
        }
        uiText.text = "";
        fullText = question.question;
        animator.Play("Talk", 1, 0f);
        animator.Play("Point", 2, 0f);
        typingCoroutine = StartCoroutine(ShowText());

        // "Randomizer" rekkefølgen til knappene, så går igjenom og bestemmer deres status som rett eller falsk svar.
        buttonsLogic = buttonsLogic.OrderBy(q => Random.value).ToList(); 
        foreach (AnswerButton button in buttonsLogic) {
        button.buttonSetup(question.answers[index], index == question.correctIndex);
            index++;
            if (index > 3) {
                index = 0;
            }
        }
        curtainAnimator.Play("Up",0,0f);
        StartCoroutine(ActivateButtons());
        } else {
            snakkebobble.SetActive(false);
            endScreen.SetActive(true);
            ScoreText.text = score + "/"+ antallSporsmal + " riktig!";
        }
    }
        private IEnumerator ActivateButtons()
    {
        // Get og slå på alle barneobjektene 
        foreach (Transform child in OptionsButtons.transform)
        {
            if (child.gameObject.activeSelf == false)  // Only activate if it's not active
            {
                child.gameObject.SetActive(true);   // Activate the button
            }
            yield return new WaitForSeconds(ButtonSpeed);  // Wait for the specified delay before activating the next one
        }

    }
    void LoadQuestionsFromJson()
    {
        // Henter jsonfil fra Resourcefolder.
        TextAsset jsonFile = Resources.Load<TextAsset>("questions");

        if (jsonFile != null)
        {
            // Henter spørsmål fra Json
            List<QuizQuestion> questions = JsonHelper.FromJson<QuizQuestion>(jsonFile.text).ToList();
            antallSporsmal = questions.Count;
            // Sorterer tilfeldig 
            questions = questions.OrderBy(q => Random.value).ToList();        
            
            // Putter på stacken
            foreach (QuizQuestion question in questions)
            {
                questionStack.Push(question);
            }
        }

        else
        {
            Debug.LogError("Quiz JSON file not found in Resources!");
        }
    }
    public void incScore(){
        score++;
    }

}
