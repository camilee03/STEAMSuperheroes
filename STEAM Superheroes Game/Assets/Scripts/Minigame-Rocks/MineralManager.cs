using TMPro;
using UnityEngine;

public class MineralManager : MonoBehaviour
{
    int numIlmenite, numAnorthosite, numMareBasalt, numHelium, numParadot;
    int totalClicks = 5;
    int numClicksUsed;
    int totalMinerals = 5;
    [SerializeField] TMP_Text infoText;
    [SerializeField] TMP_Text clickText;
    [SerializeField] WinLevel winLevel;


    private void Update()
    {
        infoText.text = "Ilmenite: " + numIlmenite
            + "\nHelium: " + numHelium;

        //+"\nAnorthosite: " + numAnorthosite
        //+ "\nMare Basalt: " + numMareBasalt
        //+ "\nParadot: " + numParadot

        clickText.text = "Resources used: " + numClicksUsed;

        if (numIlmenite + numAnorthosite + numMareBasalt + numParadot + numHelium == totalMinerals) { winLevel.currencyAmountToAdd = 5 - (numClicksUsed - 16)/2; winLevel.ActivateCanvas(); }
    }

    public void AddMineral(int mineralType)
    {
        switch (mineralType)
        {
            case 0: numIlmenite++; break;
            case 1: numAnorthosite++; break;
            case 2: numMareBasalt++; break;
            case 3: numHelium++; break;
            case 4: numParadot++; break;
        }
    }

    public void AddClick()
    {
        numClicksUsed++;
    }
}
