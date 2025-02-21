using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Collections;
using static UnityEngine.UI.Image;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;


public class ProgrammingMinigameManager : MonoBehaviour
{
    public enum CODE_COMMAND
    {
        START,
        MOVE,
        TURNLEFT,
        TURNRIGHT,
        IF,
        ELSE,
    }

    //INIT
    CodeSelectSystem selectSystem = null;

    [Header("Drone")]
    [SerializeField] GameObject dronePrefab;
    [SerializeField] GameObject drone = null;
    Rigidbody2D droneRB = null;
    [SerializeField] float droneSpeed = 1;
    [SerializeField] Transform droneOrigin;
    bool moveDrone = false;
    bool rotateDrone = false;
    int droneRotationModifier = 1;

    [Header("Satellites")]
    [SerializeField] Text satelliteText = null;
    [SerializeField] GameObject satellitePickupPrefab = null;
    [SerializeField] Transform[] satelliteOrigins = null;
    List<GameObject> satelliteParts = new List<GameObject>();

    //System
    [Header("System")]
    bool codeEditable = true;
    [SerializeField] GameObject codeBlocker = null;
    float executeWaitSeconds = 1;
    int skip = 0; //for IF

    [Header("DEBUG")]
    [SerializeField] int piecesCollected = 0;
    [SerializeField] bool playInProgress = false;
    [SerializeField] bool stopRun = false;
    [SerializeField] bool gameEnded = false;


    private void Start()
    {
        selectSystem = FindFirstObjectByType<CodeSelectSystem>();
        ResetGame();
    }
    private void Update()
    {
        if (moveDrone)
        {

        }
        if (rotateDrone)
        {

        }
    }
    public void StartRun() //Called By Button
    {
        CodeEditable = false;
        codeBlocker.SetActive(true);
        stopRun = false;
        playInProgress = true;
        ResetGame();
        StartExecutingCode();
    }
    void StartExecutingCode()
    {
        StartCoroutine(ExecuteCode()); //THIS MIGHT CAUSE MEMORY LEAKS W/ INFINITE LOOPING?
        //LoopExecutingCode();
    }
    void LoopExecutingCode()
    {
        if (stopRun)
        {
            StopRun();
        } else
        {
            Debug.Log("Looping code");
            StartExecutingCode();
        }
    }
    public void StopRun()
    {
        CodeEditable = true;
        codeBlocker.SetActive(false);
        stopRun = true;
        playInProgress = false;
        ResetGame();
    }
    public IEnumerator ExecuteCode() 
    {
        List<GameObject> codeBlocksList = selectSystem.GetCodeBlocksList();
        Debug.Log("Executing Code list in codeBlocks");
        //run the code from codeblocks
        for (int i = 0; i < codeBlocksList.Count; i++)
        {
            if (gameEnded)
            {
                WinGame();
                break;
            }
            if (stopRun)
            {
                StopRun();
                break;
            }

            CodeBlock cb = codeBlocksList[i].GetComponent<CodeBlock>();
            ParseCommand(cb.GetCommand());
            yield return new WaitForSeconds(executeWaitSeconds);
            moveDrone = false;
            rotateDrone = false;
            droneRB.linearVelocity = Vector2.zero;
        }
    }
    void ParseCommand(CODE_COMMAND codeType)
    {

        Debug.Log("Parsing command " + codeType.ToString());
        switch (codeType)
        {
            case CODE_COMMAND.START:
                Command_START();
                break;
            case CODE_COMMAND.MOVE:
                Command_MOVE();
                break;
            case CODE_COMMAND.TURNLEFT:
                Command_TURNLEFT();
                break;
            case CODE_COMMAND.TURNRIGHT:
                Command_TURNRIGHT();
                break;
            case CODE_COMMAND.IF:
                Command_IF();
                break;
            case CODE_COMMAND.ELSE:
                Command_ELSE();
                break;
        }
    }
    void Command_START()
    {
        Debug.Log("Start");
        executeWaitSeconds = .1f; //Change this later
    }
    void Command_MOVE()
    {
        Debug.Log("Move");
        executeWaitSeconds = 1; //Change this later
        droneRB.linearVelocity = Vector2.up * droneSpeed;
    }
    void Command_TURNLEFT()
    {
        Debug.Log("Turn Left");
        executeWaitSeconds = 1; //Change this later
        droneRB.linearVelocity = -Vector2.right * droneSpeed;
    }
    void Command_TURNRIGHT()
    {
        Debug.Log("Turn Right");
        executeWaitSeconds = 1; //Change this later
        droneRB.linearVelocity = Vector2.right  * droneSpeed;
    }
    void Command_IF()
    {
        Debug.Log("If");
        //executeWaitSeconds = 1; //Change this later

    }
    void Command_ELSE()
    {
        Debug.Log("Else");
        //executeWaitSeconds = 1; //Change this later

    }
    private void ResetGame()
    {
        Destroy(drone);
        drone = Instantiate(dronePrefab, droneOrigin.position, droneOrigin.rotation);
        droneRB = drone.GetComponent<Rigidbody2D>();
        drone.transform.position = droneOrigin.position;

        //satellite clear & spawn
        if (satelliteParts.Count > 0)
        {
            for(int i = 0; i <  satelliteParts.Count; i++)
            {
                Destroy(satelliteParts[i]);
            }
        }
        satelliteParts.Clear();
        for(int i = 0; i < satelliteOrigins.Length; i++)
        {
            Debug.Log("HI");
            GameObject sat = Instantiate(satellitePickupPrefab, satelliteOrigins[i].position, satelliteOrigins[i].rotation);
            satelliteParts.Add(sat);
        }
        piecesCollected = 0;
    }
    void WinGame()
    {
        //TODO
    }
    public bool CodeEditable
    {
        get { return codeEditable; }
        set { codeEditable = value; }
    }
    public void IncreasePiecesCollected(GameObject satellitePickup)
    {
        if (satelliteParts.Contains(satellitePickup))
        {
            satelliteParts.Remove(satellitePickup);
            piecesCollected++;
            satelliteText.text = "Satellite Pieces Collected: " + piecesCollected;
            if (piecesCollected >= satelliteOrigins.Length)
            {
                gameEnded = true;
            }
        }
    }
}
