using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonData : MonoBehaviour
{
    public List<Sprite> SacImages = new List<Sprite>();
    private Image imageSac;
    public List<string> SacTexts = new List<string>();
    private Text textSac;
    public int idType;

    private bool oneTime;

    private void SpawnRandomSac()
    {
        imageSac.sprite = SacImages[idType];
        textSac.text = SacTexts[idType];
    }
    // Start is called before the first frame update
    void Start()
    {
        imageSac = transform.Find("SacrificeHolder").GetComponent<Image>();
        textSac = transform.Find("SacrificeText").GetComponent<Text>();
        //Crea el Tipo de Sacrificio Aleatorio
        SpawnRandomSac();
       
    }

    // Update is called once per frame
    void Update()
    {
     

    }
}
