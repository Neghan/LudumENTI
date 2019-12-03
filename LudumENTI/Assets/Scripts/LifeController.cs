using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{
    public Image cooldownProgress;
    public Image[] hearts;
    public GridMovement vidaPJ;
    private int life;
    public Color lifeG;
    public Color lifeB;

    public void CoolDownRepresentation()
    {
        cooldownProgress.fillAmount = vidaPJ.GetCoolDown()/ vidaPJ.movementCoolDown;
    }

    void ChangeLives()
    {
        
        switch (life)
        {
            case 3:
                for(int i = 0; i< 3; i++)
                {
                    hearts[i].color = lifeG; 
                }
                break;
            case 2:
                for (int i = 0; i < 3; i++)
                {
                    hearts[i].color = lifeG;
                    if (i > 1)
                    {
                        hearts[i].color = lifeB;
                    }
                }
                break;
            case 1:
                for (int i = 0; i < 3; i++)
                {
                    hearts[i].color = lifeG;
                    if (i > 0)
                    {
                        hearts[i].color = lifeB;
                    }
                }
                break;
            case 0:
                for (int i = 0; i < 3; i++)
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
        
    }

    // Update is called once per frame
    void Update()
    {
        CoolDownRepresentation();
        life = vidaPJ.GetLife();
        ChangeLives();
    }
}
