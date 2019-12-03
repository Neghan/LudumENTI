using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
 
    private AudioSource salidaSonidoGO;

    

    public void Playagain()
    {
        salidaSonidoGO.Play();
        SceneManager.LoadScene("SampleScene");
    }
    public void MainMenu()
    {
        salidaSonidoGO.Play();
        SceneManager.LoadScene("MainMenu");
    }

    // Start is called before the first frame update
    void Start()
    {
        salidaSonidoGO = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    
}
