using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sacrificer : MonoBehaviour
{
    public AudioClip clipSacrificio;
    private AudioSource audioSacrifice;
    public List<ButtonData> ListSacrifices = new List<ButtonData>();
    public GridMovement player;
    public void Sacrifice1()
    {
        //ListSacrifices[0].idType;
        player.sacrificeDone(ListSacrifices[0].idType);
        player.EnableInput();
        audioSacrifice.PlayOneShot(clipSacrificio);
        gameObject.GetComponent<Canvas>().enabled = false;
        
    }
    public void Sacrifice2()
    {
        //ListSacrifices[1].idType;
        player.sacrificeDone(ListSacrifices[1].idType);
        player.EnableInput();
        audioSacrifice.PlayOneShot(clipSacrificio);
        gameObject.GetComponent<Canvas>().enabled = false;

    }
    public void Sacrifice3()
    {
        //ListSacrifices[2].idType;
        
        player.sacrificeDone(ListSacrifices[2].idType);
        player.EnableInput();
        audioSacrifice.PlayOneShot(clipSacrificio);
        gameObject.GetComponent<Canvas>().enabled = false;

    }
    // Start is called before the first frame update
    void Start()
    {
        audioSacrifice = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
