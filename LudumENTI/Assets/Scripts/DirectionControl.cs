using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionControl : MonoBehaviour
{

    private GridMovement refToParentScript;
    //UP(0), DOWN(1), LEFT(2), RIGHT(3)
    public SpriteRenderer[] directions;

    public Color LookingAt;
    public Color AvailableDirectons;
    public Color UnavailableDirections;

    public void RotatorDirectionsUIGround(int direction)
    {
     
        switch (direction)
        {
            case 1://IZQ
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                break;
            case 2://ABAJO
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                break;
            case 3://DCHA
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                break;
            case 0://ARRIBA
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                break;
        }
        
    }
    private void CheckWhereDirections()
    {
        if (refToParentScript.sacrificioPiernaIzquierda)
        {
            directions[2].color = UnavailableDirections;
        }else if (refToParentScript.sacrificioPiernaDerecha)
        {
            directions[3].color = UnavailableDirections;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
            refToParentScript = GetComponentInParent<GridMovement>();
            directions[0].color = LookingAt;
        for(int i=1; i < directions.Length; i++)
        {
            directions[i].color = AvailableDirectons;
        }
           
    }

    // Update is called once per frame
    void Update()
    {
       CheckWhereDirections();
    }
}
