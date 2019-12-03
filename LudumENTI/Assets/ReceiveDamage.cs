using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveDamage : MonoBehaviour
{
    private int life = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public int GetLife()
    {
        return life;
    }

    public void TakeDamage()
    {
        life--;
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
