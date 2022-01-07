using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayonVertEnigmeManager : MonoBehaviour
{
    [SerializeField] RotateOnClick[] ListAnneaux;
    [SerializeField] CURRENTCOLOR[] colorNeeded;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("RayonVertWon", 0);
    }

    // Update is called once per frame
    void Update()
    {
        bool enigmaWon = true;
        for (int i = 0; i < ListAnneaux.Length; i++)
        {
            if (ListAnneaux[i].curentColor != colorNeeded[i])
            {
                enigmaWon = false;
            }
        }
        if(enigmaWon)
        {
            PlayerPrefs.SetInt("RayonVertWon", 1);
            Debug.Log("Victory");
        }
    }
}
