using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvidiaMovement : MonoBehaviour
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
    GameObject GO, GO2, GO3, GO4, GO5, GO6, GO7, GO8;
    Vector3Int LeftCell; //Izquierda
    Vector3Int DownCell; //Abajo
    Vector3Int DownLeftCell; //Abajo-Izquierda
    Vector3Int DownRightCell; //Abajo-Derecha
    Vector3Int RightCell; // Derecha
    Vector3Int RightDownCell; // Derecha-Abajo 
    Vector3Int UpCell; // Arriba
    Vector3Int UpLeftCell; // Arriba-Izquierda
    Vector3Int UpRightCell; // Arriba-Derecha

    //Referencias
    private GameObject player;
    Vector3Int playerPos;

    public AudioClip[] akSounds;
    private AudioSource m_audioSource;
    private bool walk = false;
    private bool Cry = false;

    private Vector3Int WhereIsPlayer()
    {
        playerPos = myGrid.WorldToCell(player.GetComponent<GridMovement>().transform.position);

        LeftCell = new Vector3Int(currentCell.x - Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y, currentCell.z); //Izquierda
        DownCell = new Vector3Int(currentCell.x, currentCell.y - Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); //Abajo
        DownLeftCell = new Vector3Int(currentCell.x - Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y - Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); //Abajo-Izquierda
        DownRightCell = new Vector3Int(currentCell.x + Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y - Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); //Abajo-Derecha
        RightCell = new Vector3Int(currentCell.x + Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y, currentCell.z); // Derecha
        RightDownCell = new Vector3Int(currentCell.x + Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y - Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Derecha-Abajo 
        UpCell = new Vector3Int(currentCell.x, currentCell.y + Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Arriba
        UpLeftCell = new Vector3Int(currentCell.x - Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y + Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Arriba-Izquierda
        UpRightCell = new Vector3Int(currentCell.x + Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y + Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Arriba-Derecha

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
            if (!walk && !AlreadyAttack && !AlreadyAttack && Cry)
            {
                m_audioSource.PlayOneShot(akSounds[Random.Range(3, 4)]);///Walk
                walk = true;
            }
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

                GO = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(UpLeftCell), transform.rotation);
                GO3 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(UpRightCell), transform.rotation);
                GO6 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(DownLeftCell), transform.rotation);
                GO8 = Instantiate(warningEnemy, myGrid.GetCellCenterWorld(DownRightCell), transform.rotation);
                showWarning = true;
            }
            else if (AlreadyAttack)
            {
                
                Destroy(GO.gameObject);
                Destroy(GO3.gameObject);
                Destroy(GO6.gameObject);
                Destroy(GO8.gameObject);
                GO = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(UpLeftCell), transform.rotation);
                GO3 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(UpRightCell), transform.rotation);
                GO6 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(DownLeftCell), transform.rotation);
                GO8 = Instantiate(attackEnemy, myGrid.GetCellCenterWorld(DownRightCell), transform.rotation);

                ///Solo para que no suene el andar cuando ataca
                walk = true;
                m_audioSource.PlayOneShot(akSounds[0]);///Attack
                Attacking = false;
                AlreadyWarning = false;
                AlreadyAttack = false;
                showWarning = false;
                Destroy(GO.gameObject, movementCoolDown);
                Destroy(GO3.gameObject, movementCoolDown);
                Destroy(GO6.gameObject, movementCoolDown);
                Destroy(GO8.gameObject, movementCoolDown);
                
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
        m_audioSource = this.GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        myGrid = GameObject.Find("Room").GetComponent<Grid>();
        coolDown = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<GridMovement>().enabledInput && !Cry)
        {
            m_audioSource.PlayOneShot(akSounds[1]);///Grito
            Cry = true;
        }
        if (this.GetComponent<ReceiveDamage>().GetLife() <= 0)
        {
            Attacking = false;
            m_audioSource.PlayOneShot(akSounds[2]);///Death
            Destroy(GO.gameObject);
            Destroy(GO3.gameObject);
            Destroy(GO6.gameObject);
            Destroy(GO8.gameObject);
            Destroy(this.gameObject,4.0f);
        }

        if (player.GetComponent<GridMovement>().enabledInput && !player.GetComponent<GridMovement>().canMove && canMove)
        {
            walk = false;
            goToCell = currentCell + WhereIsPlayer();
            coolDown = movementCoolDown;

        }

        coolDownHandler();
        Attack();
        Move();
    }
}
