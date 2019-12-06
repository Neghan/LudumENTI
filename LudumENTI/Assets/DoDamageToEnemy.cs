using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamageToEnemy : MonoBehaviour
{
    private GameObject enemy;
    private bool Damage = false;

    
    

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }


    // Update is called once per frame
    void Update()
    {
        if (enemy != null)
        {
            
            if (enemy.transform.position == transform.position && !Damage)
            {
                Debug.LogWarning("Enemy dañado");
                enemy.GetComponent<ReceiveDamage>().TakeDamage();
                Damage = true;
            }
            
        }
        else
        {
            Destroy(this.gameObject);
        }
         
    }
}
