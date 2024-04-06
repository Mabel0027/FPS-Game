using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    public static EnemyCounter instance;

    public int totalEnemies;
    private int defeatedEnemies;

    public TextMeshProUGUI enemyCounterText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        defeatedEnemies = 0;
        UpdateEnemyCounterText();
    }

    public void EnemyDefeated()
    {
        defeatedEnemies++;
        UpdateEnemyCounterText();
    }

    void UpdateEnemyCounterText()
    {
        enemyCounterText.text = "Enemigos: "+ (totalEnemies - defeatedEnemies) + "/" + totalEnemies;
    }
}
