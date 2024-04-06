using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RayCast : MonoBehaviour
{
    public Transform laserOrigin; // No necesitas esto si obtienes la posición de los ojos del enemigo
    public float gunRange = 50f;
    public float fireRate = 0.2f;
    public float laserDuration = 0.05f;
    public Transform enemyEyes; // Referencia al transform de los ojos del enemigo

    LineRenderer laserLine;
    float fireTimer;

    void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
    }

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && fireTimer > fireRate)
        {
            fireTimer = 0;

            // Actualiza la posición inicial del láser para que coincida con los ojos del enemigo
            laserLine.SetPosition(0, enemyEyes.position);

            RaycastHit hit;

            if (Physics.Raycast(enemyEyes.position, enemyEyes.forward, out hit, gunRange))
            {
                laserLine.SetPosition(1, hit.point);
                // Puedes agregar acciones adicionales aquí si el láser golpea algo
            }
            else
            {
                laserLine.SetPosition(1, enemyEyes.position + (enemyEyes.forward * gunRange));
            }

            StartCoroutine(ShootLaser());
        }
    }

    IEnumerator ShootLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }
}
