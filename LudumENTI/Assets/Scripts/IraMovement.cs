using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IraMovement : MonoBehaviour
{
    //Grid
    [Range(0, 8)]
    public int rows;

    [Range(0, 7)]
    public int columns;

    public Grid myGrid;


    //Variables Publicas
    public Vector3Int startLujuriaPos;
    public float movementCoolDown = 2.0f;
    public float movementSpeed = 1.0f;

    //Movimiento
    private float coolDown = 2.0f;
    private bool canMove = false;

    private Vector3Int currentCell = new Vector3Int(-1, -1, -1);
    private Vector3Int goToCell = new Vector3Int(-1, -1, -1);

    //Ataque
    private bool Attacking = false;
    private bool AlreadyWarning = false;
    private bool AlreadyAttack = false;
    private bool showWarning = false;
    public GameObject attackEnemy;
    public GameObject warningEnemy;
    GameObject GO, GO2, GO3, GO4, GO5, GO6, GO7, GO8,GO9, GO10, GO11,GO12;
    
    private enum Dir { Iz,Der,Arr,Abj};
    Dir PlayerP = Dir.Arr;
    
   
    
    ///Ataque arriba
    Vector3Int UpCell; // Arriba
    Vector3Int Upx2Cell; // Arribax2
    Vector3Int Upx2LeftCell; // Arribax2-Izquierda
    Vector3Int Upx2RightCell; // Arribax2-Derecha
    ///Ataque derecha
    Vector3Int RightCell; // Derecha
    Vector3Int Rightx2Cell; // Derechax2
    Vector3Int Rightx2DownCell; // Derechax2-Abajo 
    Vector3Int Rightx2UpCell; // Derechax2-Arriba 
    ///Ataque Abajo
    Vector3Int DownCell; //Abajo
    Vector3Int Downx2Cell; //Abajox2
    Vector3Int Downx2LeftCell; //Abajox2-Izquierda
    Vector3Int Downx2RightCell; //Abajox2-Derecha
    ///Ataque Izquierda
    Vector3Int LeftCell; //Izquierda
    Vector3Int Leftx2Cell; //Izquierdax2
    Vector3Int Leftx2DownCell; //Izquierdax2 Abajo
    Vector3Int Leftx2UpCell; //Izquierdax2 Arriba

   
    Vector3Int DownLeftCell; //Abajo-Izquierda
    Vector3Int DownRightCell; //Abajo-Derecha
    Vector3Int RightDownCell; // Derecha-Abajo 
    Vector3Int UpLeftCell; // Arriba-Izquierda
    Vector3Int UpRightCell; // Arriba-Derecha
    //Referencias
    private GameObject player;
    Vector3Int playerPos;



    private Vector3Int WhereIsPlayer()
    {
        playerPos = myGrid.WorldToCell(player.GetComponent<GridMovement>().transform.position);

        
        DownLeftCell = new Vector3Int(currentCell.x - Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y - Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); //Abajo-Izquierda
        DownRightCell = new Vector3Int(currentCell.x + Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y - Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); //Abajo-Derecha
       
        RightDownCell = new Vector3Int(currentCell.x + Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y - Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Derecha-Abajo 
       
        UpLeftCell = new Vector3Int(currentCell.x - Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y + Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Arriba-Izquierda
        UpRightCell = new Vector3Int(currentCell.x + Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y + Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Arriba-Derecha


        ///Ataque Izquierda
        LeftCell = new Vector3Int(currentCell.x - Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y, currentCell.z); //Izquierda
        Leftx2Cell = new Vector3Int(currentCell.x -2* Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y, currentCell.z); //Izquierdax2
        Leftx2UpCell = new Vector3Int(currentCell.x - 2*Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y + Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); //Izquierdax2Arr
        Leftx2DownCell = new Vector3Int(currentCell.x -2* Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y - Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); //Izquierdax2Abj
        ///Ataque Abajo
        DownCell = new Vector3Int(currentCell.x, currentCell.y - Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); //Abajo
        Downx2Cell = new Vector3Int(currentCell.x, currentCell.y - 2*Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); //Abajox2
        Downx2LeftCell = new Vector3Int(currentCell.x - Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y -2* Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); //Abajox2-Izquierda
        Downx2RightCell = new Vector3Int(currentCell.x + Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y - 2 * Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); //Abajox2-Derecha
        ///Ataque derecha
        RightCell = new Vector3Int(currentCell.x + Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y, currentCell.z); // Derecha
        Rightx2Cell = new Vector3Int(currentCell.x + 2*Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y, currentCell.z); // Derechax2
        Rightx2DownCell = new Vector3Int(currentCell.x +2* Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y - Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Derecha-Abajo 
        Rightx2UpCell = new Vector3Int(currentCell.x + 2 * Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y + Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Derecha-Abajo 
        ///Ataque Abajo
        UpCell = new Vector3Int(currentCell.x, currentCell.y + Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Arriba
        Upx2Cell = new Vector3Int(currentCell.x, currentCell.y +2* Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Arribax2
        Upx2LeftCell = new Vector3Int(currentCell.x - Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y + 2*Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Arribax2-Izquierda
        Upx2RightCell = new Vector3Int(currentCell.x + Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y +2* Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Arribax2-Derecha

        if (!AlreadyAttack && ((playerPos == LeftCell) || (playerPos == DownCell) || (playerPos == DownLeftCell) || (playerPos == DownRightCell) || (playerPos == RightCell) || (playerPos == RightDownCell) || (playerPos == UpCell) || (playerPos == UpLeftCell) || (playerPos == UpRightCell)))
        {
            if (!AlreadyWarning)
            {
                Attacking = true;
                AlreadyWarning = true;
            }
            else
            {
                AlreadyAttack = true;
            }
            return new Vector3Int(0, 0, 0);
        }
        else if (AlreadyWarning)
        {
            AlreadyAttack = true;
            return new Vector3Int(0, 0, 0);
        }
        else
        {

            //Jugador a la izquierda
            if (currentCell.x > playerPos.x)
            {
                AlreadyAttack = false;
                return new Vector3Int(-Vector3Int.CeilToInt(myGrid.cellSize).x, 0, 0);
            }
            //Jugador a la derecha
            else if (currentCell.x < playerPos.x)
            {
                AlreadyAttack = false;
                return new Vector3Int(Vector3Int.CeilToInt(myGrid.cellSize).x, 0, 0);
            }
            //Jugador abajo
            else if (currentCell.y > playerPos.y)
            {
                AlreadyAttack = false;
                return new Vector3Int(0, -Vector3Int.CeilToInt(myGrid.cellSize).y, 0);
            }
            //Jugador arriba
            else if (currentCell.y < playerPos.y)
            {
                AlreadyAttack = false;
                return new Vector3Int(0, Vector3Int.CeilToInt(myGrid.cellSize).y, 0);
            }
            AlreadyAttack = false;
            return new Vector3Int(0, 0, 0);
        }
    }
    private void Move()
    {
        if (!Attacking)
        {
            currentCell = myGrid.WorldToCell(transform.position);
            //left
            // Debug.Log(Vector3.Distance(player.GetComponent<GridMovement>().transform.position, transform.position));
            if (Vector3.Distance(player.GetComponent<GridMovement>().transform.position, transform.position) >= 1)
                SimulateMovement(currentCell, goToCell);
        }


    }

    private void Attack()
    {
        if (Attacking)
        {
            if (AlreadyWarning && !AlreadyAttack && !showWarning)
            {
                currentCell = myGrid.WorldToCell(transform.position);
                //Jugador a la izquierda
                if (currentCell.x > playerPos.x)
                {
                    PlayerP = Dir.Iz;
                    GO = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(LeftCell), transform.rotation);
                    GO2 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(Leftx2Cell), transform.rotation);
                    GO3 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(Leftx2DownCell), transform.rotation);
                    GO4 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(Leftx2UpCell), transform.rotation);
                }
                //Jugador a la derecha
                else if (currentCell.x < playerPos.x)
                {
                    PlayerP = Dir.Der;
                    GO = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(RightCell), transform.rotation);
                    GO2 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(Rightx2Cell), transform.rotation);
                    GO3 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(Rightx2DownCell), transform.rotation);
                    GO4 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(Rightx2UpCell), transform.rotation);

                }
                //Jugador abajo
                else if (currentCell.y > playerPos.y)
                {
                    PlayerP = Dir.Abj;
                    GO = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(DownCell), transform.rotation);
                    GO2 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(Downx2Cell), transform.rotation);
                    GO3 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(Downx2LeftCell), transform.rotation);
                    GO4 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(Downx2RightCell), transform.rotation);
                }
                //Jugador arriba
                else if (currentCell.y < playerPos.y)
                {
                    PlayerP = Dir.Arr;
                    GO = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(UpCell), transform.rotation);
                    GO2 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(Upx2Cell), transform.rotation);
                    GO3 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(Upx2LeftCell), transform.rotation);
                    GO4 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(Upx2RightCell), transform.rotation);
                }
               
               
                
                showWarning = true;
            }
            else if (AlreadyAttack)
            {
                
                Destroy(GO.gameObject);
                Destroy(GO2.gameObject);
                Destroy(GO3.gameObject);
                Destroy(GO4.gameObject);
                //Jugador a la izquierda
                if (PlayerP == Dir.Iz)
                {
                    GO = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(LeftCell), transform.rotation);
                    GO2 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(Leftx2Cell), transform.rotation);
                    GO3 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(Leftx2DownCell), transform.rotation);
                    GO4 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(Leftx2UpCell), transform.rotation);
                }
                //Jugador a la derecha
                else if (PlayerP == Dir.Der)
                {
                    GO = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(RightCell), transform.rotation);
                    GO2 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(Rightx2Cell), transform.rotation);
                    GO3 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(Rightx2DownCell), transform.rotation);
                    GO4 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(Rightx2UpCell), transform.rotation);

                }
                //Jugador abajo
                else if (PlayerP == Dir.Abj)
                {
                    GO = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(DownCell), transform.rotation);
                    GO2 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(Downx2Cell), transform.rotation);
                    GO3 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(Downx2LeftCell), transform.rotation);
                    GO4 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(Downx2RightCell), transform.rotation);
                }
                //Jugador arriba
                else if (PlayerP == Dir.Arr)
                {
                    GO = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(UpCell), transform.rotation);
                    GO2 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(Upx2Cell), transform.rotation);
                    GO3 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(Upx2LeftCell), transform.rotation);
                    GO4 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(Upx2RightCell), transform.rotation);
                }
                
                Attacking = false;
                AlreadyWarning = false;
                AlreadyAttack = false;
                showWarning = false;
                Destroy(GO.gameObject, movementCoolDown);
                Destroy(GO2.gameObject, movementCoolDown);
                Destroy(GO3.gameObject, movementCoolDown);
                Destroy(GO4.gameObject, movementCoolDown);
                
            }

        }

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
        Attack();
        Move();
    }
}
