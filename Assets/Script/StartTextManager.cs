using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartTextManager : MonoBehaviour
{
    public static StartTextManager Instance;

    [Header("UI")]
    public GameObject startText;
    public GameObject finishText;

    [Header("Cubos del nivel")]
    public int totalCubes = 9;       // Cuántos cubos caen al inicio
    private int cubesLanded = 0;     // Cuántos ya aterrizaron

    public int totalBlocks = 9;      // Cuántos cubos existen para romper
    private int blocksDestroyed = 0; // Cuántos cubos ya desaparecieron

    [Header("Referencias")]
    public TimerCountdown timer;
    public PlayerInput playerInput;

    private bool gameFinished = false;

    void Awake()
    {
        Instance = this;

        if (startText != null)
            startText.SetActive(false);

        if (finishText != null)
            finishText.SetActive(false);

        // Bloqueamos movimiento al inicio
        if (playerInput != null)
            playerInput.enabled = false;
    }

    // Llamado por FallingCube cuando un cubo TERMINA DE CAER
    public void CubeLanded()
    {
        cubesLanded++;

        if (cubesLanded >= totalCubes)
        {
            StartCoroutine(StartSequence());
        }
    }

    private IEnumerator StartSequence()
    {
        if (startText != null)
            startText.SetActive(true);

        yield return new WaitForSeconds(2f);

        if (startText != null)
            startText.SetActive(false);

        // Activar tiempo
        if (timer != null)
            timer.StartTimer();

        // Activar movimiento del jugador
        if (playerInput != null)
            playerInput.enabled = true;
    }

    // Llamado por FallingCube cuando se rompe un cubo
    public void BlockDestroyed()
    {
        if (gameFinished) return;

        blocksDestroyed++;

        if (blocksDestroyed >= totalBlocks)
        {
            FinishGame();
        }
    }

    // Llamado por TimerCountdown o cuando los cubos acaban
    public void FinishGame()
    {
        if (gameFinished) return;

        gameFinished = true;

        // Bloquear movimiento
        if (playerInput != null)
            playerInput.enabled = false;

        // Detener tiempo
        if (timer != null)
            timer.StopTimer();

        // Mostrar FINISH
        if (finishText != null)
            finishText.SetActive(true);
    }
}
