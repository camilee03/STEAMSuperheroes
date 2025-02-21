using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Collections;
using static UnityEngine.UI.Image;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.UIElements;
using UnityEditor;


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

    [SerializeField] GameObject drone = null;
    Rigidbody2D droneRB = null;
    [SerializeField] float droneSpeed = 1;
    [SerializeField] Transform droneOrigin;

    bool codeEditable = true;

    float executeWaitSeconds = 1;

    CodeSelectSystem selectSystem = null;

    int skip = 0;

    [Header("DEBUG")]
    [SerializeField] int maxPiecesCollected = 3;
    [SerializeField] int piecesCollected = 0;
    [SerializeField] bool playInProgress = false;
    [SerializeField] bool stopRun = false;
    [SerializeField] bool gameEnded = false;


    private void Start()
    {
        selectSystem = FindFirstObjectByType<CodeSelectSystem>();
        droneRB = drone.GetComponent<Rigidbody2D>();
    }
    public void StartRun() //Called By Button
    {
        stopRun = false;
        playInProgress = true;
        ResetGame();
        StartCoroutine(ExecuteCode());
    }
    public void StopRun()
    {
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
        droneRB.linearVelocity = transform.up * droneSpeed;
    }
    void Command_TURNLEFT()
    {
        Debug.Log("Turn Left");
        executeWaitSeconds = 1; //Change this later
        drone.transform.Rotate(Vector3.forward, 90);
    }
    void Command_TURNRIGHT()
    {
        Debug.Log("Turn Right");
        executeWaitSeconds = 1; //Change this later
        drone.transform.Rotate(Vector3.forward, -90);
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
        drone.transform.position = droneOrigin.position;
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
    public void IncreasePiecesCollected()
    {
        piecesCollected++;
        if (piecesCollected > maxPiecesCollected)
        {
            gameEnded = true;
        }
    }
}
