using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {
    [SerializeField] private TMP_Text uiText; // Drag your UI Text here
    [SerializeField] private float typeSpeed = 0.05f; // Speed of the effect
    [SerializeField] private float ButtonSpeed = 0.05f; // Determins how long between button spawns
    [SerializeField] private Animator animator; // "Mascots" animator
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject OptionsButtons;

    private string fullText;
    private string currentText = "";
    private Coroutine typingCoroutine; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        fullText = "Welcome! Please press play to start.";
        animator.Play("Talk", 1, 0f);
        typingCoroutine = StartCoroutine(ShowText());
    }

    IEnumerator ShowText() {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            uiText.text = currentText;
            yield return new WaitForSeconds(typeSpeed);
        }
            typingCoroutine = null;
    }

    public void Click(){
        startButton.SetActive(false);
        if (typingCoroutine != null){
            StopCoroutine(typingCoroutine);
            uiText.text = fullText;
        }
        uiText.text = "";
        fullText = "Pasienten veier 60 kg, og det oppgitte distribusjonsvolumet for digoksin er 7 L/kg. Hva er pasientens totale distribusjonsvolum for digoksin?";
        animator.Play("Talk", 1, 0f);
        animator.Play("Point", 2, 0f);
        typingCoroutine = StartCoroutine(ShowText());

        StartCoroutine(ActivateButtons());
    }
        private IEnumerator ActivateButtons()
    {
        // Get all child buttons of the parent object
        foreach (Transform child in OptionsButtons.transform)
        {
            if (child.gameObject.activeSelf == false)  // Only activate if it's not active
            {
                child.gameObject.SetActive(true);   // Activate the button
            }

            yield return new WaitForSeconds(ButtonSpeed);  // Wait for the specified delay before activating the next one
        }

    }
}
