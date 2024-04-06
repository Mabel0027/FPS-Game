using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectCounter : MonoBehaviour
{
    public static ObjectCounter instance;

    public int totalObjects;
    private int remainingObjects;
    private List<GameObject> destroyedObjects = new List<GameObject>(); // Lista de objetos destruidos

    public TextMeshProUGUI objectCounterText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        remainingObjects = totalObjects;
        UpdateObjectCounterText();
    }

    public void ObjectDestroyed(GameObject destroyedObject)
    {
        if (!destroyedObjects.Contains(destroyedObject)) // Verifica si el objeto ya está en la lista de objetos destruidos
        {
            remainingObjects--;
            destroyedObjects.Add(destroyedObject); // Agrega el objeto a la lista de objetos destruidos
            UpdateObjectCounterText();
        }
    }

    void UpdateObjectCounterText()
    {
        objectCounterText.text = "Objetos: "+ remainingObjects + "/" + totalObjects;
    }
}
