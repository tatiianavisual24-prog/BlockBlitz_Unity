using UnityEngine;
using TMPro;

public class TimerCountdown : MonoBehaviour
{
    public TMP_Text timerText;
    public float timeRemaining = 60f;

    private bool timerRunning = false;

    void Awake()
    {
        if (timerText == null)
            timerText = GetComponent<TMP_Text>();

        UpdateDisplay();

        // Empieza oculto
        if (timerText != null)
            timerText.gameObject.SetActive(false);
    }

    public void StartTimer()
    {
        timerRunning = true;

        if (timerText != null)
            timerText.gameObject.SetActive(true);
    }

    public void StopTimer()
    {
        timerRunning = false;

        
        if (timerText != null)
            timerText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!timerRunning)
            return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            timerRunning = false;

            // Avisar al manager que se acabó el tiempo
            if (StartTextManager.Instance != null)
            {
                StartTextManager.Instance.FinishGame();
            }
        }

        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        if (timerText != null)
            timerText.text = Mathf.Ceil(timeRemaining).ToString();
    }
}
