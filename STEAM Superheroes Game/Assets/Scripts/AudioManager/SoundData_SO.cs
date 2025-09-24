using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public enum BgmTrack
{
    None,
    MAINMENU,
    PREMINIGAME,
    MINING,
    PROGRAMMING,
    CIRCUIT,
    ART,
    MATH
}

[CreateAssetMenu(fileName ="SoundDataObject", menuName = "Scriptable Objects/SoundDataObject")]
public class SoundData_SO : ScriptableObject
{
    [Header("-------- Music --------")]
    public AudioClip mainMenuBgm;
    public AudioClip preMinigameBgm;
    public AudioClip miningMinigameBgm;
    public AudioClip programmingMinigameBgm;
    public AudioClip circuitMinigameBgm;
    public AudioClip artMinigameBgm;
    public AudioClip mathMinigameBgm;


    [Header("-------- Sounds --------")]

    [Header("General")]
    public AudioClip levelComplete;
    public AudioClip buttonClick;

    [Header("Art Minigame")]
    public AudioClip shapeGrabbed;
    public AudioClip shapeReleased;

    [Header("Circuit Minigame")]
    public AudioClip logicGateOptionsOpened;
    public AudioClip logicGateOptionSelected;

    [Header("Math Minigame")]
    public AudioClip paintSelected;
    public AudioClip paintApplied;

    [Header("Mining Minigame")]
    public AudioClip clickHexagon;
    public AudioClip mineralCollected;

    [Header("Satellite Minigame")]
    public AudioClip satelliteCollected;
    public AudioClip droneMoving;
}
