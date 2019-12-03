using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    public SpriteRenderer[] hearts;
    private ReceiveDamage vidaEnemy;
    private int life;
    public Color lifeG;
    public Color lifeB;

    void ChangeLives()
    {

        switch (life)
        {
            case 5:
                for (int i = 0; i < hearts.Length; i++)
                {
                    hearts[i].color = lifeG;
                }
                break;
            case 4:
                for (int i = 0; i < hearts.Length; i++)
                {
                    hearts[i].color = lifeG;
                    if (i > 3)
                    {
                        hearts[i].color = lifeB;
                    }
                }
                break;
            case 3:
                for (int i = 0; i < hearts.Length; i++)
                {
                    hearts[i].color = lifeG;
                    if (i > 2)
                    {
                        hearts[i].color = lifeB;
                    }
                }
                break;
            case 2:
                for (int i = 0; i < hearts.Length; i++)
                {
                    hearts[i].color = lifeG;
                    if (i > 1)
                    {
                        hearts[i].color = lifeB;
                    }
                }
                break;
            case 1:
                for (int i = 0; i < hearts.Length; i++)
                {
                    hearts[i].color = lifeG;
                    if (i > 0)
                    {
                        hearts[i].color = lifeB;
                    }
                }
                break;
            case 0:
                for (int i = 0; i < hearts.Length; i++)
                {
                    hearts[i].color = lifeB;
                }
                break;
            default:
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        vidaEnemy = GetComponentInParent<ReceiveDamage>();
    }

    // Update is called once per frame
    void Update()
    {
        
        life = vidaEnemy.GetLife();
        ChangeLives();
    }
}
