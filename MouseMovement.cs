using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{


public float mouseSensitivity = 500f;

float xRotation = 0f;
float yRotation = 0f;

public float topClamp = -90f;
public float bottomClamp = 90f;


    void Start()
    {
        //Bloquea el curson en medio de la pantalla y hacerlo invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Obteniendo las entradas el mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Rotación en le eje x (arriba y abajo)
        xRotation -= mouseY;

        //Sujetar la rotación
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        //Rotación en le eje y (derecha e izquierda)
        yRotation += mouseX;

        //Aplicar las rotaciones nuestro transform
        transform.localRotation = Quaternion.Euler(xRotation, yRotation,0f);

    }
}
