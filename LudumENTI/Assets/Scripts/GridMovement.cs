using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    [Range(0, 8)]
    public int rows;

    [Range(0, 7)]
    public int columns;


    public Grid myGrid;
    public Vector3Int startPlayerPos;

    private float coolDown = 2.0f;
    public bool canMove;

    public float movementCoolDown = 2.0f;
    public float movementSpeed = 1.0f;

    private Vector3Int currentCell = new Vector3Int(-1, -1, -1);
    private Vector3Int goToCell = new Vector3Int(-1, -1, -1);

    /////////////ARREGLAR/////////////
    private bool checkCollisionTileTypeWalkable()
    {
        return true;
    }

    private void Move()
    {
        //left
        if (Input.GetKeyDown(KeyCode.A) && canMove && checkCollisionTileTypeWalkable())
        {
            //Move the player to the left cell position;
            currentCell = myGrid.WorldToCell(transform.position);
            goToCell = new Vector3Int(currentCell.x - Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y, currentCell.z);
            coolDown = movementCoolDown;
        }
        //right
        else if (Input.GetKeyDown(KeyCode.D) && canMove)
        {
            //Move the player to the right cell position;
            currentCell = myGrid.WorldToCell(transform.position);
            goToCell = new Vector3Int(currentCell.x + Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y, currentCell.z);
            coolDown = movementCoolDown;
        }
        //up
        else if (Input.GetKeyDown(KeyCode.W) && canMove)
        {
            //Move the player to the up cell position;
            currentCell = myGrid.WorldToCell(transform.position);
            goToCell = new Vector3Int(currentCell.x, currentCell.y + Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z);
            coolDown = movementCoolDown;
        }
        //down
        else if (Input.GetKeyDown(KeyCode.S) && canMove)
        {
            //Move the player to the down cell position;
            currentCell = myGrid.WorldToCell(transform.position);
            goToCell = new Vector3Int(currentCell.x, currentCell.y - Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z);
            coolDown = movementCoolDown;
        }

        SimulateMovement(currentCell, goToCell);
    }


    private void SimulateMovement(Vector3Int currentCell, Vector3Int goToCell)
    {
        //Limits of the movement.
        if (myGrid.GetCellCenterWorld(goToCell).x >= -rows && myGrid.GetCellCenterWorld(goToCell).x <= rows &&
            myGrid.GetCellCenterWorld(goToCell).y >= 0 && myGrid.GetCellCenterWorld(goToCell).y <= columns)
        {
            //Move the player smoothly to the cell position. ///Check cooldown and valid position.
            if (currentCell != new Vector3Int(-1, -1, -1) && goToCell != new Vector3Int(-1, -1, -1))
            {
                //Cooldown Move
                coolDown -= Time.deltaTime;

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


    void Start()
    {
        //Posicionar al player en la posición inicial.
        transform.position = myGrid.GetCellCenterWorld(startPlayerPos);
        coolDown = 0.0f;
    }

    void Update()
    {
        //Checks if you are able to move.
        coolDownHandler();

        Move();

    }
}
