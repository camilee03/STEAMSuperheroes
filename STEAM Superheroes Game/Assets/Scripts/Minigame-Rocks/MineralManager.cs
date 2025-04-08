using TMPro;
using UnityEngine;

public class MineralManager : MonoBehaviour
{
    int numIlmenite, numAnorthosite, numMareBasalt, numHelium, numParadot;
    int totalClicks = 5;
    int numClicksRemaining;
    [SerializeField] TMP_Text infoText;
    [SerializeField] TMP_Text clickText;


    private void Start()
    {
        numClicksRemaining = totalClicks;
    }

    private void Update()
    {
        infoText.text = "Ilmenite: " + numIlmenite
            + "\nAnorthosite: " + numAnorthosite
            + "\nMare Basalt: " + numMareBasalt
            + "\nParadot: " + numParadot;

        clickText.text = "Clicks remaining: " + numClicksRemaining;
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

    public void RemoveClick()
    {
        numClicksRemaining--;
    }
}
