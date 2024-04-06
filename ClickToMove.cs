using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToMove : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent navAgent;
    private Transform playerTransform;

    private void Start()
    {
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Busca el jugador por su etiqueta
    }

    private void Update()
    {
        // Mueve el agente hacia la posición del jugador
navAgent.SetDestination(playerTransform.position);
    }
}
