using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveDamage : MonoBehaviour
{
    private AudioSource m_audioSource;
    public AudioClip akSound;
    private int life = 5;
    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = this.GetComponent<AudioSource>();
        
    }

    public int GetLife()
    {
        return life;
    }

    public void TakeDamage()
    {
        life--;
        m_audioSource.PlayOneShot(akSound);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
