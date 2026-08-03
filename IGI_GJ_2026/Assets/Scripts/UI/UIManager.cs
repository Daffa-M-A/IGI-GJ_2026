using UnityEngine;
using TMPro; 

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Elements")]
    public TextMeshProUGUI timerText;
    public GameObject winScreen;
    public GameObject loseScreen;

    [Header("Document UI")]
    public GameObject documentPanel;
    public TextMeshProUGUI documentContentText;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void UpdateTimerDisplay(float currentTime)
    {
        timerText.text = currentTime.ToString("F2") + "s";
    }

    public void ShowWinScreen()
    {
        winScreen.SetActive(true);
        timerText.gameObject.SetActive(false);
    }

    public void ShowLoseScreen()
    {
        loseScreen.SetActive(true);
        Invoke("HideLoseScreen", 2f); 
    }

    private void HideLoseScreen()
    {
        loseScreen.SetActive(false);
    }

    public void ShowDocument(string content)
    {
        documentPanel.SetActive(true);
        documentContentText.text = content;
    }

    public void HideDocument()
    {
        documentPanel.SetActive(false);
    }
}