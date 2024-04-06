using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerHealth playerHealth; // referencia al script PlayerHealth del jugador

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisionó es un enemigo
        if (other.CompareTag("Zombie"))
        {
            // Llama a la función TakeDamage del script PlayerHealth
            playerHealth.TakeDamage(1); // Cambia el valor del daño según sea necesario
        }
    }
}
