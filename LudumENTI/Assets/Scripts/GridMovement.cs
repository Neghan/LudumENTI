using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GridMovement : MonoBehaviour
{
    public GameObject particlesDamage;
    [Range(0, 8)]
    public int rows;

    [Range(0, 7)]
    public int columns;

    public GameObject attackPlayer;
    public GameObject CegueraDer;
    public GameObject CegueraIzq;
    public Grid myGrid;
    public Vector3Int startPlayerPos;

    private float coolDown = 2.0f;
    private int direction = 0;
    public bool canMove;
    private bool enabledInput;
    public float movementCoolDown = 2.0f;
    public float movementSpeed = 1.0f;

    private Vector3Int currentCell = new Vector3Int(-1, -1, -1);
    private Vector3Int goToCell = new Vector3Int(-1, -1, -1);

    private int life = 3;
    public bool sacrificioBrazoDerecho = false;
    public bool sacrificioBrazoIzquierdo = false;
    public bool sacrificioPiernaIzquierda = false;
    public bool sacrificioPiernaDerecha = false;
    GameObject GO, GO2, GO3, GO4, GO5;

    private SpriteRenderer mySprite;

    public GameObject Player;
    public List<Sprite> PlayerSprites;

    public GameObject PauseMenu_GO;
    public GameObject Gameover_GO;

    public int GetLife()
    {
        return life; 
    }
    public void StopInput()
    {
        enabledInput = false;
    }
    public void EnableInput()
    {
        enabledInput = true;
    }
    public void ResetLife()
    {
        life = 3;
    }


    public void SetLocation(Vector3Int Location)
    {

        currentCell= goToCell = Location;
        Debug.Log("currentCell: "+currentCell);
        Debug.Log("goToCell: "+goToCell);
    }

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
                Player.GetComponent<SpriteRenderer>().sprite = PlayerSprites[2];
                break;

            case 2:
                goToCell = new Vector3Int(currentCell.x, currentCell.y - Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); //Abajo
                Player.GetComponent<SpriteRenderer>().sprite = PlayerSprites[0];
                break;
            case 3:
                goToCell = new Vector3Int(currentCell.x + Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y, currentCell.z); // Derecha
                Player.GetComponent<SpriteRenderer>().sprite = PlayerSprites[3];
                
                break;

            case 0:
                goToCell = new Vector3Int(currentCell.x, currentCell.y + Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Arriba
                Player.GetComponent<SpriteRenderer>().sprite = PlayerSprites[1];
                break;

            default:
                break;
        }
    }

    public int getDirection()
    {
        return direction;
    }


    private void Attack()
    {
        currentCell = myGrid.WorldToCell(transform.position);


        Vector3Int LeftCell = new Vector3Int(currentCell.x - Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y, currentCell.z); //Izquierda
        Vector3Int DownCell = new Vector3Int(currentCell.x, currentCell.y - Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); //Abajo
        Vector3Int DownLeftCell = new Vector3Int(currentCell.x - Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y - Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); //Abajo-Izquierda
        Vector3Int DownRightCell = new Vector3Int(currentCell.x + Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y - Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); //Abajo-Derecha
        Vector3Int RightCell = new Vector3Int(currentCell.x + Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y, currentCell.z); // Derecha
        Vector3Int RightDownCell = new Vector3Int(currentCell.x + Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y - Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Derecha-Abajo 
        Vector3Int UpCell = new Vector3Int(currentCell.x, currentCell.y + Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Arriba
        Vector3Int UpLeftCell = new Vector3Int(currentCell.x - Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y + Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Arriba-Izquierda
        Vector3Int UpRightCell = new Vector3Int(currentCell.x + Vector3Int.CeilToInt(myGrid.cellSize).x, currentCell.y + Vector3Int.CeilToInt(myGrid.cellSize).y, currentCell.z); // Arriba-Derecha

        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (direction)
            {
                case 1: //Atacar a la izquierda
                        //Sin brazos

                    GO = Instantiate(attackPlayer, myGrid.GetCellCenterWorld(LeftCell), transform.rotation);
                    //Sin brazo izquierdo
                    if (!sacrificioBrazoDerecho)
                    {
                        GO2 = Instantiate(attackPlayer, myGrid.GetCellCenterWorld(UpCell), transform.rotation);
                        GO3 = Instantiate(attackPlayer, myGrid.GetCellCenterWorld(UpLeftCell), transform.rotation);
                    }
                    //Sin brazo derecho
                    if (!sacrificioBrazoIzquierdo)
                    {
                        GO4 = Instantiate(attackPlayer, myGrid.GetCellCenterWorld(DownCell), transform.rotation);
                        GO5 = Instantiate(attackPlayer, myGrid.GetCellCenterWorld(DownLeftCell), transform.rotation);
                    }

                    //CheckDamage()                    
                    Destroy(GO.gameObject, 0.5f);
                    if (!sacrificioBrazoDerecho)
                    {
                        Destroy(GO2.gameObject, 0.5f);
                        Destroy(GO3.gameObject, 0.5f);
                    }
                    if (!sacrificioBrazoIzquierdo)
                    {
                        Destroy(GO4.gameObject, 0.5f);
                        Destroy(GO5.gameObject, 0.5f);
                    }
                    coolDown = movementCoolDown;
                    break;


                case 2: //Atacar hacia abajo
                    //Sin brazos
                    GO = Instantiate(attackPlayer, myGrid.GetCellCenterWorld(DownCell), transform.rotation);
                    //Sin brazo izquierdo
                    if (!sacrificioBrazoDerecho)
                    {
                        GO2 = Instantiate(attackPlayer, myGrid.GetCellCenterWorld(LeftCell), transform.rotation);
                        GO3 = Instantiate(attackPlayer, myGrid.GetCellCenterWorld(DownLeftCell), transform.rotation);
                    }
                    //Sin brazo derecho
                    if (!sacrificioBrazoIzquierdo)
                    {
                        GO4 = Instantiate(attackPlayer, myGrid.GetCellCenterWorld(RightCell), transform.rotation);
                        GO5 = Instantiate(attackPlayer, myGrid.GetCellCenterWorld(RightDownCell), transform.rotation);
                    }


                    Destroy(GO.gameObject, 0.5f);
                    if (!sacrificioBrazoDerecho)
                    {
                        Destroy(GO2.gameObject, 0.5f);
                        Destroy(GO3.gameObject, 0.5f);
                    }
                    if (!sacrificioBrazoIzquierdo)
                    {
                        Destroy(GO4.gameObject, 0.5f);
                        Destroy(GO5.gameObject, 0.5f);
                    }
                    coolDown = movementCoolDown;
                    break;

                case 3: //Atacar a la derecha

                    //Sin brazos
                    GO = Instantiate(attackPlayer, myGrid.GetCellCenterWorld(RightCell), transform.rotation);
                    //Sin brazo izquierdo
                    if (!sacrificioBrazoDerecho)
                    {
                        GO2 = Instantiate(attackPlayer, myGrid.GetCellCenterWorld(DownCell), transform.rotation);
                        GO3 = Instantiate(attackPlayer, myGrid.GetCellCenterWorld(DownRightCell), transform.rotation);
                    }
                    //Sin brazo derecho
                    if (!sacrificioBrazoIzquierdo)
                    {
                        GO4 = Instantiate(attackPlayer, myGrid.GetCellCenterWorld(UpCell), transform.rotation);
                        GO5 = Instantiate(attackPlayer, myGrid.GetCellCenterWorld(UpRightCell), transform.rotation);
                    }


                    Destroy(GO.gameObject, 0.5f);
                    if (!sacrificioBrazoDerecho)
                    {
                        Destroy(GO2.gameObject, 0.5f);
                        Destroy(GO3.gameObject, 0.5f);
                    }
                    if (!sacrificioBrazoIzquierdo)
                    {
                        Destroy(GO4.gameObject, 0.5f);
                        Destroy(GO5.gameObject, 0.5f);
                    }
                    coolDown = movementCoolDown;
                    break;

                case 0: //Atacar hacia arriba

                    //Sin brazos
                    GO = Instantiate(attackPlayer, myGrid.GetCellCenterWorld(UpCell), transform.rotation);
                    //Sin brazo izquierdo
                    if (!sacrificioBrazoDerecho)
                    {
                        GO2 = Instantiate(attackPlayer, myGrid.GetCellCenterWorld(RightCell), transform.rotation);
                        GO3 = Instantiate(attackPlayer, myGrid.GetCellCenterWorld(UpRightCell), transform.rotation);
                    }
                    //Sin brazo derecho
                    if (!sacrificioBrazoIzquierdo)
                    {
                        GO4 = Instantiate(attackPlayer, myGrid.GetCellCenterWorld(LeftCell), transform.rotation);
                        GO5 = Instantiate(attackPlayer, myGrid.GetCellCenterWorld(UpLeftCell), transform.rotation);
                    }


                    Destroy(GO.gameObject, 0.5f);
                    if (!sacrificioBrazoDerecho)
                    {
                        Destroy(GO2.gameObject, 0.5f);
                        Destroy(GO3.gameObject, 0.5f);
                    }
                    if (!sacrificioBrazoIzquierdo)
                    {
                        Destroy(GO4.gameObject, 0.5f);
                        Destroy(GO5.gameObject, 0.5f);
                    }
                    coolDown = movementCoolDown;
                    break;

            }
        }

    }

    IEnumerator parpadeoDamage()
    {
        mySprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        mySprite.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        mySprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        mySprite.color = Color.white;
    }

    public void TakeDamage()
    {
        
            life--;
            Instantiate(particlesDamage, transform.position, Quaternion.identity);
            StartCoroutine(parpadeoDamage());
            if (life <= 0)
            {
                Destroy(this.gameObject);
                Gameover_GO.SetActive(true);
            }
        
    }

    private void Move()
    {
        //left
        if (Input.GetKeyDown(KeyCode.A) && canMove && checkCollisionTileTypeWalkable())
        {
            //left
            if (Input.GetKeyDown(KeyCode.A) && canMove && checkCollisionTileTypeWalkable() && !sacrificioPiernaIzquierda)
            {
                //Move the player to the left cell position;
                currentCell = myGrid.WorldToCell(transform.position);
                 
                direction++;
                if (direction > 3)
                {
                    direction = 0;
                }

                seeDirectionMoving(direction);

                coolDown = movementCoolDown;
            }
        }
        //right
        else if (Input.GetKeyDown(KeyCode.D) && canMove && !sacrificioPiernaDerecha)
        {
            //Move the player to the right cell position;
            currentCell = myGrid.WorldToCell(transform.position);
             
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                life--;

                if (life <= 0)
                {
                   Gameover_GO.SetActive(true); 
                }
            }
        }

        public void sacrificeDone(int part)
        {
            switch (part)
            {
                case 0:
                    //Pierna Izquierda
                    sacrificioPiernaIzquierda = true;
                    break;
                case 1:
                    //Pierna Derecha
                    sacrificioPiernaDerecha = true;
                    break;
                case 2:
                    //Brazo Izquierdo
                    sacrificioBrazoIzquierdo = true;
                    break;
                case 3:
                    //Brazo Derecho
                    sacrificioBrazoDerecho = true;
                    break;
                case 4:
                //Ojo Izquierdo
                CegueraIzq.SetActive(true);
                    break;
                case 5:
                //Ojo Derecho
                CegueraDer.SetActive(true);
                    break;
                default:
                    break;
            }
        }

        void PauseMenu()
        {
        if (Input.GetKeyDown(KeyCode.P))
            {
            PauseMenu_GO.SetActive(true);
            Time.timeScale = 0;
            }
        }

        void Start()
        {
        mySprite = GetComponentInChildren<SpriteRenderer>();
            //Posicionar al player en la posición inicial.
            transform.position = myGrid.GetCellCenterWorld(startPlayerPos);
            coolDown = 0.0f;
            CegueraDer.SetActive(false);
            CegueraIzq.SetActive(false);
            Gameover_GO.SetActive(false);
        }

        void Update()
        {
        //Checks if you are able to move.
        if (enabledInput)
        {
            coolDownHandler();
        }
            

            if (sacrificioPiernaIzquierda && sacrificioPiernaDerecha)
            {
                sacrificioPiernaDerecha = sacrificioPiernaIzquierda = false;
            }
            Attack();
        //TakeDamage();
            PauseMenu();
            Move();
        }
    
}
