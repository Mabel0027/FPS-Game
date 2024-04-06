using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;

    public TextMeshProUGUI healthDisplay;

    private GameManager gameManager;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthDisplay();

        gameManager = GameManager.instance; // Obtener referencia al GameManager
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthDisplay();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Aquí puedes agregar la lógica para cuando el jugador muere
        Debug.Log("El jugador ha muerto");

        gameManager.EndGame(false); // Indicar al GameManager que el jugador ha perdido
    }

    void UpdateHealthDisplay()
    {
        if (healthDisplay != null)
        {
            healthDisplay.text = "Vida: " + currentHealth.ToString();
        }
    }
}
