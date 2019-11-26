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

    //private float coolDown = 2.0f;
    private bool canMove;
    private bool moved;
    //public float movementCoolDown = 2.0f;
    public float movementSpeed = 1.0f;

    private Vector3Int currentCell = new Vector3Int(-1, -1, -1);
    private Vector3Int goToCell = new Vector3Int(-1, -1, -1);

    private GameObject player;

     private Vector3Int WhereIsPlayer()
    {
        Vector3Int playerPos = player.GetComponent<GridMovement>().currentCell;
        //Jugador a la izquierda
        if (currentCell.x>playerPos.x)
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
        return new Vector3Int(0,0,0);
    }
    private void Move()
    {
        //left
        if (!player.GetComponent<GridMovement>().canMove)
        {
            //Move the player to the left cell position;
            currentCell = myGrid.WorldToCell(transform.position);
            goToCell = currentCell+WhereIsPlayer();
            //coolDown = movementCoolDown;
        }
        
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
                //Cooldown Move
               // coolDown -= Time.deltaTime;

                //Interpolate the movement
                transform.position = Vector3.MoveTowards(transform.position, myGrid.GetCellCenterWorld(goToCell), movementSpeed * Time.deltaTime);
            }

        }

    }
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
       // coolDownHandler();

        Move();
    }
}
