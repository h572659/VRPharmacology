using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class MouseHoverScript : MonoBehaviour
{
    private TMP_Text buttonText;


    void Start()
    {
        buttonText = GetComponentInChildren<TMP_Text>();
    }

    public void OnPointerEnter()  
    {
        buttonText.color = Color.red;
    }

    public void OnPointerExit()
    {
        buttonText.color = Color.white;
    }

    public void OnClick()
    {
        Debug.Log("Button clicked!");
    }
}

