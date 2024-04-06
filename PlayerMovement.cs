using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float gorundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;

    bool isGrounded;
    bool isMoving;

    private Vector3 lastPosition = new Vector3(0f,0f,0f);

    void Start()
    {
        controller = GetComponent<CharacterController>();

    }

    void Update()
    {
        //Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, gorundDistance, groundMask);

        //Resetear la velocidad por defaul
        if(isGrounded && velocity.y<0)
        {
            velocity.y = -2f;
        }

        //Obteniendo las entradas
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        //Creando movimiento del vector
        Vector3 move = transform.right * x + transform.forward * y;


        //Mover al jugador
        controller.Move(move * speed * Time.deltaTime);

        //Verificar si el jugador puede saltar 
        if(Input.GetButtonDown("Jump")&& isGrounded)
        {
            //Jugador corriendo
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //Caer al suelo
        velocity.y +=gravity * Time.deltaTime;

        //Ejecutar el salto
        controller.Move(velocity * Time.deltaTime);

        if(lastPosition !=gameObject.transform.position && isGrounded == true)
        {
            isMoving = true;
        }else{

            isMoving = false;
        }

        lastPosition = gameObject.transform.position;
    }
}
