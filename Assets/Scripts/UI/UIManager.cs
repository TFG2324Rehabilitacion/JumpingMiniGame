using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider movementSlider;
    [SerializeField] private GameObject connectionPanel;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI ipText;
    [SerializeField] private PreparationCountdownTimer timer;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private GameObject ipPanel;
    [SerializeField] private Image arrowIndicator;
    [SerializeField] private Sprite upArrow;
    [SerializeField] private Sprite downArrow;
    [SerializeField] private GameObject endGamePanel;

    private static UIManager instance;
    public static UIManager Instance
    {

        get { return instance; }
        private set
        {
            if (instance == null)
            {
                instance = value;
            }
            else if (instance != value)
            {
                Destroy(value);
            }
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
    }

    void Update()
    {

    }

    public void UpdateSlider(float value)
    {
        movementSlider.value = value;

        if (value >= 1f && InputManager.Instance.movementDone)
        {
            movementSlider.fillRect.GetComponent<Image>().color = Color.blue;
            arrowIndicator.sprite = downArrow;
        }
        else if (!InputManager.Instance.movementDone)
        {
            movementSlider.fillRect.GetComponent<Image>().color = Color.yellow;
            arrowIndicator.sprite = upArrow;
        }
        else
        {
            movementSlider.fillRect.GetComponent<Image>().color = Color.red;

        }
    }

    public void SetIPText()
    {
        ipText.SetText($"{ipText.text} {NetworkManager.Instance.ipAddress}");
    }


    public void StartCountdown(float countdown)
    {
        ipPanel.SetActive(false);

        timer.Init(countdown);
        timer.gameObject.SetActive(true);
    }

    public void StopCountdown()
    {
        ipPanel.SetActive(true);

        timer.gameObject.SetActive(false);
    }
    public void EnableConnectionPanel()
    {
        connectionPanel.SetActive(true);
    }

    public void DisableConnectionPanel()
    {
        connectionPanel.SetActive(false);
    }

    public void UpdateScore()
    {
        scoreText.SetText($"{GameManager.Instance.GetScore()}");
    }

    public void UpdateRoundsText()
    {
        roundText.SetText($"Rondas: {GameManager.Instance.GetRoundsLeft()}");
    }

    public void ShowEndGamePanel()
    {
        endGamePanel.SetActive(true);
    }
}
