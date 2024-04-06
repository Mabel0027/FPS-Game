using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public List<Rigidbody> allParts = new List<Rigidbody>();

    public void Shatter()
    {
        foreach(Rigidbody part in allParts)
        {
            part.isKinematic = false;
        }
        ObjectCounter.instance.ObjectDestroyed(gameObject); // Pasar una referencia al objeto que está siendo destruido


    }
}
