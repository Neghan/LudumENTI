using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvariciaMovement : MonoBehaviour
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
    GameObject GO, GO2, GO3, GO4, GO5, GO6, GO7, GO8, GO9, GO10, GO11, GO12,GO13,GO14,GO15,GO16,GO17;
    Vector3Int LeftCell2; ///Izquierda
    Vector3Int UpLeftCell2; /// Arriba-Izquierdax2
    Vector3Int UpLeftCell; /// Arriba-Izquierda
    Vector3Int UpUpLeftCell; /// Arribax2-Izquierda
    Vector3Int UpCell2; /// Arribax2
    Vector3Int UpUpRightCell; /// Arribax2-Derecha
    Vector3Int UpRightCell; /// Arriba-Derecha
    Vector3Int UpRightCell2; /// Arriba-Derechax2
    Vector3Int RightCell2; /// Derechax2
    Vector3Int DownRightCell2; ///Abajo-Derechax2
    Vector3Int DownRightCell; ///Abajo-Derecha
    Vector3Int DownDownRightCell; ///Abajox2-Derecha
    Vector3Int DownCell2; ///Abajox2
    Vector3Int DownDownLeftCell; ///Abajox2-Izquierda
    Vector3Int DownLeftCell; ///Abajo-Izquierda
    Vector3Int DownLeftCell2; //Abajo-Izquierdax2

    

    Vector3Int LeftCell; //Izquierda
    Vector3Int DownCell; //Abajo
    //Vector3Int DownLeftCell; //Abajo-Izquierda
    //Vector3Int DownRightCell; //Abajo-Derecha
    Vector3Int RightCell; // Derecha
    Vector3Int RightDownCell; // Derecha-Abajo 
    Vector3Int UpCell; // Arriba
    //Vector3Int UpLeftCell; // Arriba-Izquierda
   // Vector3Int UpRightCell; // Arriba-Derecha


    //Referencias
    private GameObject player;
    Vector3Int playerPos;



    private Vector3Int WhereIsPlayer()
    {
        playerPos = myGrid.WorldToCell(player.GetComponent<GridMovement>().transform.position);

        LeftCell2 = new Vector3Int(currentCell.x - 2 * Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y, currentCell.z); //Izquierdax2
        UpLeftCell2 = new Vector3Int(currentCell.x - 2 * Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y + Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Arriba-Izquierda
        UpLeftCell = new Vector3Int(currentCell.x - Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y + Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Arriba-Izquierda
        UpUpLeftCell = new Vector3Int(currentCell.x - Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y + 2 * Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Arribax2-Izquierda
        UpCell2 = new Vector3Int(currentCell.x, currentCell.y + 2 * Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Arribax2
        UpUpRightCell = new Vector3Int(currentCell.x + Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y + 2 * Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Arribax2-Derecha
        UpRightCell = new Vector3Int(currentCell.x + Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y + Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Arriba-Derecha
        UpRightCell2 = new Vector3Int(currentCell.x + 2 * Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y + Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Arriba-Derechax2
        RightCell2 = new Vector3Int(currentCell.x + 2 * Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y, currentCell.z); // Derechax2
        DownRightCell2 = new Vector3Int(currentCell.x + 2 * Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y - Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); //Abajo-Derechax2
        DownRightCell = new Vector3Int(currentCell.x + Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y - Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); //Abajo-Derecha
        DownDownRightCell = new Vector3Int(currentCell.x + Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y - 2 * Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); //Abajox2-Derecha
        DownCell2 = new Vector3Int(currentCell.x, currentCell.y - 2 * Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); //Abajox2
        DownDownLeftCell = new Vector3Int(currentCell.x - 2 * Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y - Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); //Abajox2-Izquierda
        DownLeftCell = new Vector3Int(currentCell.x - Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y - Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); //Abajo-Izquierda
        DownLeftCell2 = new Vector3Int(currentCell.x - Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y - 2 * Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); //Abajo-Izquierdax2


        LeftCell = new Vector3Int(currentCell.x - Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y, currentCell.z); //Izquierda
        DownCell = new Vector3Int(currentCell.x, currentCell.y - Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); //Abajo
        
        
        RightCell = new Vector3Int(currentCell.x + Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y, currentCell.z); // Derecha
        RightDownCell = new Vector3Int(currentCell.x + Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y - Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Derecha-Abajo 
        UpCell = new Vector3Int(currentCell.x, currentCell.y + Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Arriba
        
        


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
                GO = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(LeftCell2), transform.rotation);
                GO2 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(UpLeftCell2), transform.rotation);
                GO3 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(UpLeftCell), transform.rotation);
                GO4 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(UpUpLeftCell), transform.rotation);
                GO5 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(UpCell2), transform.rotation);
                GO6 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(UpUpRightCell), transform.rotation);
                GO7 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(UpRightCell), transform.rotation);
                GO8 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(UpRightCell2), transform.rotation);
                GO9 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(RightCell2), transform.rotation);
                GO10 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(DownRightCell2), transform.rotation);
                GO11 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(DownRightCell), transform.rotation);
                GO12 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(DownDownRightCell), transform.rotation);
                GO13 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(DownCell2), transform.rotation);
                GO14 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(DownDownLeftCell), transform.rotation);
                GO15 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(DownLeftCell), transform.rotation);
                GO16 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(DownLeftCell2), transform.rotation);
                showWarning = true;
            }
            else if (AlreadyAttack)
            {

                Destroy(GO.gameObject);
                Destroy(GO2.gameObject);
                Destroy(GO3.gameObject);
                Destroy(GO4.gameObject);
                Destroy(GO5.gameObject);
                Destroy(GO6.gameObject);
                Destroy(GO7.gameObject);
                Destroy(GO8.gameObject);
                Destroy(GO9.gameObject);
                Destroy(GO10.gameObject);
                Destroy(GO11.gameObject);
                Destroy(GO12.gameObject);
                Destroy(GO13.gameObject);
                Destroy(GO14.gameObject);
                Destroy(GO15.gameObject);
                Destroy(GO16.gameObject);

                GO = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(LeftCell2), transform.rotation);
                GO2 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(UpLeftCell2), transform.rotation);
                GO3 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(UpLeftCell), transform.rotation);
                GO4 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(UpUpLeftCell), transform.rotation);
                GO5 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(UpCell2), transform.rotation);
                GO6 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(UpUpRightCell), transform.rotation);
                GO7 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(UpRightCell), transform.rotation);
                GO8 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(UpRightCell2), transform.rotation);
                GO9 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(RightCell2), transform.rotation);
                GO10 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(DownRightCell2), transform.rotation);
                GO11 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(DownRightCell), transform.rotation);
                GO12 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(DownDownRightCell), transform.rotation);
                GO13 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(DownCell2), transform.rotation);
                GO14 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(DownDownLeftCell), transform.rotation);
                GO15 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(DownLeftCell), transform.rotation);
                GO16 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(DownLeftCell2), transform.rotation);

                Attacking = false;
                AlreadyWarning = false;
                AlreadyAttack = false;
                showWarning = false;
                Destroy(GO.gameObject, movementCoolDown);
                Destroy(GO2.gameObject, movementCoolDown);
                Destroy(GO3.gameObject, movementCoolDown);
                Destroy(GO4.gameObject, movementCoolDown);
                Destroy(GO5.gameObject, movementCoolDown);
                Destroy(GO6.gameObject, movementCoolDown);
                Destroy(GO7.gameObject, movementCoolDown);
                Destroy(GO8.gameObject, movementCoolDown);
                Destroy(GO9.gameObject, movementCoolDown);
                Destroy(GO10.gameObject, movementCoolDown);
                Destroy(GO11.gameObject, movementCoolDown);
                Destroy(GO12.gameObject, movementCoolDown);
                Destroy(GO13.gameObject, movementCoolDown);
                Destroy(GO14.gameObject, movementCoolDown);
                Destroy(GO15.gameObject, movementCoolDown);
                Destroy(GO16.gameObject, movementCoolDown);

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
        myGrid = GameObject.Find("Room").GetComponent<Grid>();
        coolDown = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<ReceiveDamage>().GetLife() <= 0)
        {

            Destroy(GO.gameObject);
            Destroy(GO2.gameObject);
            Destroy(GO3.gameObject);
            Destroy(GO4.gameObject);
            Destroy(GO5.gameObject);
            Destroy(GO6.gameObject);
            Destroy(GO7.gameObject);
            Destroy(GO8.gameObject);
            Destroy(GO9.gameObject);
            Destroy(GO10.gameObject);
            Destroy(GO11.gameObject);
            Destroy(GO12.gameObject);
            Destroy(GO13.gameObject);
            Destroy(GO14.gameObject);
            Destroy(GO15.gameObject);
            Destroy(GO16.gameObject);
            Destroy(this.gameObject);
        }

        if (player.GetComponent<GridMovement>().enabledInput && !player.GetComponent<GridMovement>().canMove && canMove)
        {

            goToCell = currentCell + WhereIsPlayer();
            coolDown = movementCoolDown;

        }

        coolDownHandler();
        Attack();
        Move();
    }
}
