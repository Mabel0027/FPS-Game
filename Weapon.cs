using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Weapon : MonoBehaviour
{

    private Animator animator;
    public bool isAciveWeapon;
    public int weaponDamage;

    //Disparo
    [Header("Shooting")]
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;

    //Burst
    [Header("Burst")]
    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;


    //Spread
    [Header("Spread")]

    public float spreadIntensity;

    //Loading
    public float reloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReloading;


    //Propiedades de la bala
    [Header("Bullet")]

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30;
    public float bulletPrefabLifeTime = 3f;

    //UI
    public TextMeshProUGUI ammoDisplay;


    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    public ShootingMode currentShootingMode;


    private void Awake(){

        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
        animator = GetComponent<Animator>();

        bulletsLeft = magazineSize;
    }

    void Update()
    {

        if(bulletsLeft == 0 && isShooting)
        {
            SoundManager.Instance.EmptySound.Play();
        }


//manteniendo presionada la parte inferior izquierda del mouse
        if(currentShootingMode == ShootingMode.Auto){

            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if (currentShootingMode == ShootingMode.Single || 
                currentShootingMode == ShootingMode.Burst)
                {
                    //HAciendo click ene l boton izquierdo del mouse una vez
                    isShooting = Input.GetKeyDown(KeyCode.Mouse0);

                }

        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && isReloading == false)
        {
            Reload();
        }

        //If you want to automatically reload when magazine is empty
        if(readyToShoot && isShooting == false && isReloading== false && bulletsLeft<=0)
        {
           // Reload();
        }


        if(readyToShoot && isShooting && bulletsLeft>0){

            burstBulletsLeft = bulletsPerBurst;
            FireWeapon();

        }

        if(AmmoManager.Instance.ammoDisplay !=null)
        {
            AmmoManager.Instance.ammoDisplay.text = $"Balas: {bulletsLeft/bulletsPerBurst}/{magazineSize/bulletsPerBurst}";

        }

    }

private void FireWeapon()
{
    
    bulletsLeft--;

    readyToShoot = false;

    Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;
    animator.SetTrigger("Recoil");
    SoundManager.Instance.shootingSound.Play();
    // Instanciar la bala
    GameObject bullet = Instantiate(this.bulletPrefab, this.bulletSpawn.position, Quaternion.identity);

    // Obtener el componente Bullet del objeto de la bala instanciada
    Bullet bul = bullet.GetComponent<Bullet>();

    // Establecer el daño de la bala al daño del arma
    bul.bulletDamage = this.weaponDamage;

    // Apuntar la bala para que mire en la dirección de disparo
    bullet.transform.forward = shootingDirection;

    // Disparar la bala aplicando una fuerza
    bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * this.bulletVelocity, ForceMode.Impulse);

    // Destruir la bala después de un cierto tiempo
    StartCoroutine(DestroyBulletAfterTime(bullet, this.bulletPrefabLifeTime));

    // Comprobar si hemos terminado de disparar
    if (this.allowReset)
    {
        Invoke("ResetShot", this.shootingDelay);
        this.allowReset = false;
    }

    // Modo ráfaga
    if (this.currentShootingMode == ShootingMode.Burst && this.burstBulletsLeft > 1)
    {
        this.burstBulletsLeft--;
        Invoke("FireWeapon", this.shootingDelay);
    }
}

private void Reload()
{
    SoundManager.Instance.ReloadingSound.Play();
    isReloading = true;
    Invoke("ReloadCompleted", reloadTime);
}

private void ReloadCompleted()
{
    bulletsLeft = magazineSize;
    isReloading = false;
}

private void ResetShot(){
    readyToShoot = true;
    allowReset = true;
}

public Vector3 CalculateDirectionAndSpread()
{
    //Ahooting from the middle of the screen to check where we are pointing at
Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
    RaycastHit hit;

    Vector3 targetPoint;
    if(Physics.Raycast(ray, out hit))
    {
        //Hititng something
        targetPoint = hit.point;
    }
    else
    {
        //Shooting at the air
        targetPoint = ray.GetPoint(100);
    }

    Vector3 direction = targetPoint - bulletSpawn.position;

    float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

    float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

    //Returning the shooting direction and spread
    return direction + new Vector3(x,y,0);

}

    private IEnumerator DestroyBulletAfterTime (GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}

