using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpSacrifices : MonoBehaviour
{

    private ButtonData[] childrenSacrificeButtons;

    private int lastRandom = 0;
    private int currentRandom = 0;

    private void SetUpChildrensID()
    {
       
        foreach (ButtonData childrenSac in childrenSacrificeButtons){

            do
            {
                currentRandom = Random.Range(0, childrenSac.SacImages.Count);
            }
            while (currentRandom == lastRandom);
            childrenSac.idType = currentRandom;

            lastRandom = currentRandom;
            //Crea el Tipo de Sacrificio Aleatorio
            childrenSac.SpawnRandomSac();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        childrenSacrificeButtons = GetComponentsInChildren<ButtonData>();
        SetUpChildrensID();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
