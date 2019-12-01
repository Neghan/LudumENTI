using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamageToPlayer : MonoBehaviour
{
    private GameObject player;
    private bool Damage = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        
    }

    
    // Update is called once per frame
    void Update()
    {
    

        if(player.transform.position == transform.position && !Damage)
        {
            Debug.LogWarning("Player dañado");
            player.GetComponent<GridMovement>().TakeDamage();
            Damage = true;
        }
    }
}
