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
    public void TakeDamage()
    {
        life--;
        if (life <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
