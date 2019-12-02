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
