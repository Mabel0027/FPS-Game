using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int bulletDamage;

    private void OnCollisionEnter(Collision objectWeHit)
    {
        if(objectWeHit.gameObject.CompareTag("Target"))
        {
            print("hit " + objectWeHit.gameObject.name + " !");
            CreateBulletImpactEffect(objectWeHit);
            Destroy(gameObject);
        }

        if(objectWeHit.gameObject.CompareTag("Wall"))
        {
            print("hit a wall" );
            CreateBulletImpactEffect(objectWeHit);
            Destroy(gameObject);
        }

        if(objectWeHit.gameObject.CompareTag("Box"))
        {
            print("hit a Box" );
            objectWeHit.gameObject.GetComponent<Box>().Shatter();

            //We will not destroy the bullet on impact, it will get destroyed according to its lifetime

        }

        if(objectWeHit.gameObject.CompareTag("Barrel"))
        {
            print("hit a Barrel" );
            objectWeHit.gameObject.GetComponent<Barrel>().Shatter();

            //We will not destroy the bullet on impact, it will get destroyed according to its lifetime

        }

        if(objectWeHit.gameObject.CompareTag("Zombie"))
        {
            objectWeHit.gameObject.GetComponent<EnemyScript>().TakeDamage(bulletDamage);
            Destroy(gameObject);

        }
    }

void CreateBulletImpactEffect(Collision objectWeHit)
{
    ContactPoint contact  = objectWeHit.contacts[0];

    GameObject hole = Instantiate(

        GlobalReferences.Instance.bulletImpactEffectPrefab,
            contact.point, 
            Quaternion.LookRotation(contact.normal)

    );

    hole.transform.SetParent(objectWeHit.gameObject.transform);
}

}


