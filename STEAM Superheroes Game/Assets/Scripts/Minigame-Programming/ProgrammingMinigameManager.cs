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
using TMPro;


public class ProgrammingMinigameManager : MonoBehaviour
{
    public enum CODE_COMMAND
    {
        START,
        MOVEUP,
        MOVELEFT,
        MOVERIGHT,
        MOVEDOWN,
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

    [Header("Satellites")]
    [SerializeField] TextMeshProUGUI satelliteText = null;
    [SerializeField] GameObject satellitePickupPrefab = null;
    [SerializeField] Transform[] satelliteOrigins = null;
    List<GameObject> satelliteParts = new List<GameObject>();
    [SerializeField] Transform pickupsContainer = null;

    //System
    [Header("System")]
    bool codeEditable = true;
    [SerializeField] GameObject codeBlocker = null;
    float executeWaitSeconds = 1;
    int skip = 0; //for IF
    CODE_COMMAND previousCommand;

    [Header("UI")]
    [SerializeField] WinLevel winLevel = null;

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
    public void StartRun() //Called By Button
    {
        CodeEditable = false;
        //codeBlocker.SetActive(true);
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
            StopRun(true);
        } else
        {
            Debug.Log("Looping code");
            StartExecutingCode();
        }
    }
    public void StopRun(bool reset)
    {
        CodeEditable = true;
        //codeBlocker.SetActive(false);
        stopRun = true;
        playInProgress = false;
        if (reset) {
            ResetGame();
        }
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
                StopRun(true);
                break;
            }

            CodeBlock cb = codeBlocksList[i].GetComponent<CodeBlock>();
            ParseCommand(cb.GetCommand());
            yield return new WaitForSeconds(executeWaitSeconds);
            moveDrone = false;
            droneRB.linearVelocity = Vector2.zero;
        }
    }
    void ParseCommand(CODE_COMMAND codeType)
    {

        Debug.Log("Parsing command " + codeType.ToString());
        switch (codeType)
        {
            case CODE_COMMAND.START:
                previousCommand = CODE_COMMAND.START;
                Command_START();
                break;
            case CODE_COMMAND.MOVEUP:
                previousCommand = CODE_COMMAND.MOVEUP;
                Command_MOVEUP();
                break;
            case CODE_COMMAND.MOVELEFT:
                previousCommand = CODE_COMMAND.MOVELEFT;
                Command_MOVELEFT();
                break;
            case CODE_COMMAND.MOVERIGHT:
                previousCommand = CODE_COMMAND.MOVERIGHT;
                Command_MOVERIGHT();
                break;
            case CODE_COMMAND.MOVEDOWN:
                previousCommand = CODE_COMMAND.MOVEDOWN;
                Command_MOVEDOWN();
                break;
            case CODE_COMMAND.IF:
                previousCommand = CODE_COMMAND.IF;
                Command_IF();
                break;
            case CODE_COMMAND.ELSE:
                previousCommand = CODE_COMMAND.ELSE;
                Command_ELSE();
                break;
        }
    }
    void Command_START()
    {
        Debug.Log("Start");
        executeWaitSeconds = .1f; //Change this later
    }
    void Command_MOVEUP()
    {
        Debug.Log("Move");
        executeWaitSeconds = 1; //Change this later
        droneRB.linearVelocity = Vector2.up * droneSpeed;
    }
    void Command_MOVELEFT()
    {
        Debug.Log("Turn Left");
        executeWaitSeconds = 1; //Change this later
        droneRB.linearVelocity = -Vector2.right * droneSpeed;
    }
    void Command_MOVERIGHT()
    {
        Debug.Log("Turn Right");
        executeWaitSeconds = 1; //Change this later
        droneRB.linearVelocity = Vector2.right * droneSpeed;
    }
    void Command_MOVEDOWN() {
        Debug.Log("Turn Right");
        executeWaitSeconds = 1; //Change this later
        droneRB.linearVelocity = -Vector2.up * droneSpeed;
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
            GameObject sat = Instantiate(satellitePickupPrefab, satelliteOrigins[i].position, satelliteOrigins[i].rotation);
            sat.transform.parent = pickupsContainer.transform;
            satelliteParts.Add(sat);
        }
        piecesCollected = 0;
    }
    void WinGame()
    {
        gameEnded = true;
        StopRun(false);
        winLevel.ActivateCanvas();
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
                WinGame();
            }
        }
    }
}
