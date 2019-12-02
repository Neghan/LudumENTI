using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pauseGO;
    void Start()
    {
        pauseGO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseGO.SetActive(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

}
