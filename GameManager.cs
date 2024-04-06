using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float totalTime = 120f; // Tiempo total en segundos
    private float timeLeft; // Tiempo restante
    public TextMeshProUGUI timerText; // Referencia al Text UI para mostrar el temporizador
    public TextMeshProUGUI gameResultText; // Referencia al Text UI para mostrar el resultado del juego

    private int playerLives = 3; // Vidas del jugador
    private bool gameEnded = false; // Indica si el juego ha terminado

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        timeLeft = totalTime;
        UpdateTimerText();
    }

    private void Update()
    {
        if (!gameEnded)
        {
            timeLeft -= Time.deltaTime; // Reduce el tiempo restante cada segundo
            UpdateTimerText();

            if (timeLeft <= 0f)
            {
                EndGame(false); // Has perdido (se acabó el tiempo)
            }
        }
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeLeft / 60f);
            int seconds = Mathf.FloorToInt(timeLeft % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void PlayerDamaged()
    {
        playerLives--;
        if (playerLives <= 0)
        {
            EndGame(false); // Has perdido (se quedó sin vidas)
        }
    }

    public void EndGame(bool victory)
    {
        if (gameEnded)
            return;

        gameEnded = true;
        if (victory)
        {
            gameResultText.text = "¡Has ganado!";
            // Aquí puedes ejecutar cualquier acción adicional para la victoria
        }
        else
        {
            gameResultText.text = "¡Has perdido!";
            // Aquí puedes ejecutar cualquier acción adicional para la derrota

            // Cargar la escena del menú principal después de un breve retraso
            Invoke("LoadMainMenuScene", 6f);
        }


    }

    void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

        void LoadNivel2()
    {
        SceneManager.LoadScene("Nivel2");
    }
}
