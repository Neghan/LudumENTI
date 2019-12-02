using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject credits_GO;

    // Start is called before the first frame update
    void Start()
    {
        credits_GO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");

    } 
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        credits_GO.SetActive(true);
    }
    public void CloseCredits()
    {
        credits_GO.SetActive(false);
    }

}
