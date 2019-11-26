using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sacrificer : MonoBehaviour
{
    public List<ButtonData> ListSacrifices = new List<ButtonData>();
    public void Sacrifice1()
    {
        //ListSacrifices[0].idType;
        Debug.Log(ListSacrifices[0].idType);
        GetComponent<Canvas>().enabled = false;
    }
    public void Sacrifice2()
    {
        //ListSacrifices[1].idType;
        Debug.Log(ListSacrifices[1].idType);
        GetComponent<Canvas>().enabled = false;
    }
    public void Sacrifice3()
    {
        //ListSacrifices[2].idType;
        Debug.Log(ListSacrifices[2].idType);
        GetComponent<Canvas>().enabled = false;
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
