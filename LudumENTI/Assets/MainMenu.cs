using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject credits_GO;
    public AudioSource playSound;
    public AudioSource creditsSound;
    public AudioSource QuitSound;
    bool selectedPlay;
    // Start is called before the first frame update
    void Start()
    {
        credits_GO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator PlayGameCoroutine()
    {
        playSound.Play();
        yield return new WaitForSeconds(2.2f);
        SceneManager.LoadScene("SampleScene");
    }
    public void StartGame()
    {
        if (!selectedPlay)
        {
            selectedPlay = true;
            StartCoroutine(PlayGameCoroutine());
        }
        
    } 
    public void QuitGame()
    {
        QuitSound.Play();
        if (!selectedPlay)
            Application.Quit();
    }

    public void Credits()
    {
        creditsSound.Play();
        if (!selectedPlay)
            credits_GO.SetActive(true);
    }
    public void CloseCredits()
    {
        creditsSound.Play();
        if (!selectedPlay)
            credits_GO.SetActive(false);
    }

}
