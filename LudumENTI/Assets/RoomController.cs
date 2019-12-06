using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class RoomController : MonoBehaviour
{
    public int whichCorruptionLevel = 0;
    private Tilemap[] mySelfGroundTilemaps;

    public GameObject DoorsHolder;
    public GameObject BordersHolder;
    public GameObject LateralFencesHolder;
    public GameObject FrontalFencesHolder;

    //SPRITES
    public Sprite[] Doors;
    public Sprite[] OpenDoors;
    public Sprite[] FrontalFenceCorruptions;
    public Sprite[] LateralFenceCorruptions;

   


    private SpriteRenderer[] BordersSprites;
    private SpriteRenderer[] DoorsSprites;
    private SpriteRenderer[] LateralFences;
    private SpriteRenderer[] FrontalFences;


    //Colores de Corrupciones (Suelo y Nubes)
    public Color[] ColorCorruptions;


    public GameObject endGame;

    //Cambia los colores de las nubes y Tiles
    private void ChangeCorruptionColor()
    {
        BordersSprites = BordersHolder.GetComponentsInChildren<SpriteRenderer>();
        //CambioColor en Nubes
        foreach (SpriteRenderer cloud in BordersSprites)
        {
            cloud.color = ColorCorruptions[whichCorruptionLevel];
        }


        //Cambio color en Tiles
        mySelfGroundTilemaps[0].color = ColorCorruptions[whichCorruptionLevel]; 
        mySelfGroundTilemaps[1].color = ColorCorruptions[whichCorruptionLevel]; 
    }

    /// LLAMAR A ESTA FUNCION PARA SUBIR LA CORRUPCION DE LA SALA
    public void IncrementCorruption()
    {
        whichCorruptionLevel =(whichCorruptionLevel + 1)%7;
        ConvertToCorruption(whichCorruptionLevel);
        if(whichCorruptionLevel == 6)
        {
            endGame.SetActive(true);
        }
    }

    private void ConvertToCorruption(int which)
    {
        DoorsSprites = DoorsHolder.GetComponentsInChildren<SpriteRenderer>();
        LateralFences = LateralFencesHolder.GetComponentsInChildren<SpriteRenderer>();
        FrontalFences = FrontalFencesHolder.GetComponentsInChildren<SpriteRenderer>();

        //Puerta de Abajo
        DoorsSprites[0].sprite = Doors[which];
        //Puerta de Abrriba
        DoorsSprites[1].sprite = OpenDoors[which];

        foreach (SpriteRenderer currentFence in LateralFences)
        {
            currentFence.sprite = LateralFenceCorruptions[which];
        }
        foreach (SpriteRenderer currentFence in FrontalFences)
        {
            currentFence.sprite = FrontalFenceCorruptions[which];
        }

        //Change Color In Tiles
        ChangeCorruptionColor();
    }

    void Start()
    {
        mySelfGroundTilemaps = GetComponentsInChildren<Tilemap>();
        ConvertToCorruption(whichCorruptionLevel);
    }

    void Update()
    {
       
    }
}
