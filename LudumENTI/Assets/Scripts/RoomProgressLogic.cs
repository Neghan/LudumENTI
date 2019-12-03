using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomProgressLogic : MonoBehaviour
{
    public GameObject RoomCorruption;
    public GameObject Enemy;
    public GameObject[] ALLEnemies;
    public GameObject Player;
    public GameObject Sacrificer;


    private ParticleSystem OpenDoor;
    private Text roomText;
    public Animation backgroundAnimationPlayer;
    public Animation textAnimationPlayer;
    private bool availableToGoDown;

    private bool oneTime;

    IEnumerator GoDownRoom()
    {
        roomText.text = "Room 1";
        backgroundAnimationPlayer.Play();
        textAnimationPlayer.Play();
        yield return new WaitForSeconds(1.0f);
        //RESETEAR TODO
        RoomCorruption.GetComponent<RoomController>().IncrementCorruption();
        int corruptLevel = RoomCorruption.GetComponent<RoomController>().whichCorruptionLevel;
        Player.transform.GetComponent<GridMovement>().SetLocation(new Vector3Int(0,-4,0));
        Player.transform.GetComponent<GridMovement>().StopInput();
        Player.transform.Translate(new Vector3Int(0,-4,0));
        
        //Player.transform.GetComponent<GridMovement>().ResetLife();
        
        Enemy = Instantiate(ALLEnemies[corruptLevel]);


        
        availableToGoDown = false;
        OpenDoor.Stop();
        yield return new WaitForSeconds(2.0f);
        Sacrificer.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (availableToGoDown)
        {
            if (collision.transform.CompareTag("Player"))
            {
               StartCoroutine(GoDownRoom());
            }

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        OpenDoor =GetComponent<ParticleSystem>();
        roomText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy == null)
        {
            availableToGoDown = true;

            if (!oneTime)
            {
                OpenDoor.Play();
                oneTime = true;
            }
            
        }
    }
}
