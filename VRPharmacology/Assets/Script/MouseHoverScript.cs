using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class MouseHoverScript : MonoBehaviour
{
    private TMP_Text buttonText;
    private Color oldColor;
    private SoundManager sounds;

    private void Awake() {
        sounds = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }
    void Start()
    {
        buttonText = GetComponentInChildren<TMP_Text>();
        oldColor = buttonText.color;
    }

    public void OnPointerEnter()  
    {
        buttonText.color = Color.red;
        sounds.PlaySFX(sounds.hoverSound);
    }

    public void OnPointerExit()
    {
        buttonText.color = oldColor;
    }

    public void OnClick()
    {
        Debug.Log("Button clicked!");
    }
}

