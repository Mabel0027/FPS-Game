using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoManager : MonoBehaviour
{
    public TextMeshProUGUI ammoDisplay;

    public static AmmoManager Instance {get;set;}
    //public AmmoManager bulletImpactEffectPrefab;

    private void Awake()
    {
        if(Instance !=null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
