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
    public bool isFinalLevel;


    private void Update()
    {
        if (isFinalLevel) 
        {
            infoText.text = "Triangle: " + numIlmenite
                + "\nHalf Circle: " + numHelium
                + "\nOther: " + numMareBasalt;
        }
        else
        {
            infoText.text = "Ilmenite: " + numIlmenite
                + "\nHelium: " + numHelium;

            //+"\nAnorthosite: " + numAnorthosite
            //+ "\nMare Basalt: " + numMareBasalt
            //+ "\nParadot: " + numParadot

            if (numIlmenite + numAnorthosite + numMareBasalt + numParadot + numHelium == totalMinerals) 
            { winLevel.currencyAmountToAdd = totalClicks - (numClicksUsed - 16) / 2; winLevel.ActivateCanvas(); }
        }

        clickText.text = "Resources used: " + numClicksUsed;
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
