using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonData : MonoBehaviour
{
    public List<Sprite> SacImages = new List<Sprite>();
    public Image imageSac;
    public List<string> SacTexts = new List<string>();
    public Text textSac;
    public int idType;

    private bool oneTime;

    public void SpawnRandomSac()
    {
        imageSac.sprite = SacImages[idType];
        textSac.text = SacTexts[idType];
    }
    // Start is called before the first frame update
    void Start()
    {
  
       
    }

    // Update is called once per frame
    void Update()
    {
     

    }
}
