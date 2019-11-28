using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridMovement : MonoBehaviour
{
    [Range (0, 8)]
    public int rows;

    [Range(0, 7)]
    public int columns;

    public GameObject attackPlayer;

    public Grid myGrid;
    public Vector3Int startPlayerPos;

    private float coolDown = 2.0f;
    private bool canMove;
    private int direction = 0;
    public float movementCoolDown = 2.0f;
    public  float movementSpeed = 1.0f;

    private Vector3Int currentCell = new Vector3Int(-1, -1, -1);
    private Vector3Int goToCell = new Vector3Int(-1, -1, -1);

    /////////////ARREGLAR/////////////
    private bool checkCollisionTileTypeWalkable()
    {
       /* Vector3Int finalPos = new Vector3Int(currentCell.x - Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y, currentCell.z);
        if (myGrid.WorldToCell(finalPos) == myGrid.gameObject.transform.GetChild(1).tag == "NOWalk")*/
        return true;
    }

    public void seeDirectionMoving(int d)
    {
        switch (d)
        {
            case 1:
                goToCell = new Vector3Int(currentCell.x - Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y, currentCell.z); //Izquierda
                break;

            case 2:
                goToCell = new Vector3Int(currentCell.x, currentCell.y - Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); //Abajo
                break;
            case 3:
                goToCell = new Vector3Int(currentCell.x + Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y, currentCell.z); // Derecha
                break;

            case 0:
                goToCell = new Vector3Int(currentCell.x, currentCell.y + Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Arriba
                break;

            default:
                break;
        }
    }

    private void Attack()
    {
        currentCell = myGrid.WorldToCell(transform.position);
        
        Debug.Log(currentCell);
        Vector3Int LeftCell = new Vector3Int(currentCell.x - Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y, currentCell.z); //Izquierda
        Vector3Int DownCell = new Vector3Int(currentCell.x, currentCell.y - Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); //Abajo
        Vector3Int RightCell = new Vector3Int(currentCell.x + Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y, currentCell.z); // Derecha
        Vector3Int UpCell = new Vector3Int(currentCell.x, currentCell.y + Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Arriba

        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (direction)
            {
                case 1:
                    GameObject GO = Instantiate(attackPlayer,myGrid.GetCellCenterWorld(LeftCell), transform.rotation);
                    //CheckDamage()                    
                    Destroy(GO.gameObject,0.5f);
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
                case 0:
                    Instantiate(attackPlayer, myGrid.GetCellCenterWorld(UpCell), transform.rotation);
                    break;

            }
        }

    }

    private void Move()
        {
        //left
        if (Input.GetKeyDown(KeyCode.A) && canMove && checkCollisionTileTypeWalkable())
        {
            //Move the player to the left cell position;
            currentCell = myGrid.WorldToCell(transform.position);

            transform.Rotate(Vector3.forward * 90);
            direction++;
            if(direction >3)
            {
                direction = 0;
            }

            seeDirectionMoving(direction);

            coolDown = movementCoolDown;
            }
            //right
            else if (Input.GetKeyDown(KeyCode.D) && canMove)
            {
                //Move the player to the right cell position;
                currentCell = myGrid.WorldToCell(transform.position);
            
            transform.Rotate(Vector3.forward * -90);
            direction--;
            if (direction < 0)
            {
                direction = 3;
            }
            seeDirectionMoving(direction);

            coolDown = movementCoolDown;
        }
            //up
            else if (Input.GetKeyDown(KeyCode.W) && canMove)
            {
                //Move the player to the up cell position;
                currentCell = myGrid.WorldToCell(transform.position);
                
                seeDirectionMoving(direction);
                coolDown = movementCoolDown;
        }
            //down
            else if (Input.GetKeyDown(KeyCode.S) && canMove)
            {
                //Move the player to the down cell position;
                currentCell = myGrid.WorldToCell(transform.position);
            int nuevadireccion = 0;
            switch (direction)
            {
                case 1:
                    nuevadireccion = 3;
                    break;
                case 2:
                    nuevadireccion = 0;
                    break;
                case 3:
                    nuevadireccion = 1;
                    break;
                case 0:
                    nuevadireccion = 2;
                    break;

            }
                seeDirectionMoving(nuevadireccion);
                coolDown = movementCoolDown;
        }

        SimulateMovement(currentCell,goToCell);
    }


    private void SimulateMovement( Vector3Int currentCell,Vector3Int goToCell)
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

        Attack();

        Move();
    }
}
