using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public GameObject END;
    // Start is called before the first frame update
    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    void Start()
    {
        END.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
