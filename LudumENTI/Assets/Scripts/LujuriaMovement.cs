using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LujuriaMovement : MonoBehaviour
{
    [Range(0, 8)]
    public int rows;

    [Range(0, 7)]
    public int columns;


    public Grid myGrid;
    public Vector3Int startLujuriaPos;

    private float coolDown = 2.0f;
    public float movementCoolDown = 2.0f;
    private bool canMove = false;
  
    //public float movementCoolDown = 2.0f;
    public float movementSpeed = 1.0f;

    private Vector3Int currentCell = new Vector3Int(-1, -1, -1);
    private Vector3Int goToCell = new Vector3Int(-1, -1, -1);

    private GameObject player;

    private Vector3Int WhereIsPlayer()
    {
        Vector3Int playerPos = myGrid.WorldToCell(player.GetComponent<GridMovement>().transform.position);
        //Jugador a la izquierda
        if (currentCell.x > playerPos.x)
        {
            return new Vector3Int(-Vector3Int.CeilToInt(myGrid.cellSize).x, 0, 0);
        }
        //Jugador a la derecha
        else if (currentCell.x < playerPos.x)
        {
            return new Vector3Int(Vector3Int.CeilToInt(myGrid.cellSize).x, 0, 0);
        }
        //Jugador abajo
        else if (currentCell.y > playerPos.y)
        {
            return new Vector3Int(0, -Vector3Int.CeilToInt(myGrid.cellSize).y, 0);
        }
        //Jugador arriba
        else if (currentCell.y < playerPos.y)
        {
            return new Vector3Int(0, Vector3Int.CeilToInt(myGrid.cellSize).y, 0);
        }
        return new Vector3Int(0, 0, 0);
    }
    private void Move()
    {
        currentCell = myGrid.WorldToCell(transform.position);
        //left
       // Debug.Log(Vector3.Distance(player.GetComponent<GridMovement>().transform.position, transform.position));
        if (Vector3.Distance(player.GetComponent<GridMovement>().transform.position,transform.position) >=1)
        SimulateMovement(currentCell, goToCell);

    }
    private void SimulateMovement(Vector3Int currentCell, Vector3Int goToCell)
    {
        //Limits of the movement.
        if (myGrid.GetCellCenterWorld(goToCell).x >= -rows && myGrid.GetCellCenterWorld(goToCell).x <= rows &&
            myGrid.GetCellCenterWorld(goToCell).y >= -1 && myGrid.GetCellCenterWorld(goToCell).y <= columns)
        {
            //Move the player smoothly to the cell position. ///Check cooldown and valid position.
            if (currentCell != new Vector3Int(-1, -1, -1) && goToCell != new Vector3Int(-1, -1, -1))
            {
                
                //Interpolate the movement
                transform.position = Vector3.MoveTowards(transform.position, myGrid.GetCellCenterWorld(goToCell), movementSpeed * Time.deltaTime);
            }

        }

    }

    void coolDownHandler()
    {

        if (coolDown > 0.0f)
        {
            canMove = false;
            coolDown -= Time.deltaTime;
        }
        if (coolDown < 0.0f)
        {
            coolDown = 0.0f;
        }

        if (coolDown == 0.0f)
        {
            
            canMove = true;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        coolDown = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
       

        if (!player.GetComponent<GridMovement>().canMove && canMove)
        {
                        
            goToCell = currentCell + WhereIsPlayer();
            coolDown = movementCoolDown;
            
        }

        coolDownHandler();
        
        Move();
    }
}
