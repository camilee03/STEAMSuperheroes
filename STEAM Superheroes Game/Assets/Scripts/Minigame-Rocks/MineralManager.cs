using TMPro;
using UnityEngine;

public class MineralManager : MonoBehaviour
{
    int numIlmenite, numAnorthosite, numMareBasalt, numHelium, numParadot;
    TMP_Text infoText;

    private void Start()
    {
        infoText = GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        infoText.text = "Ilmenite: " + numIlmenite
            + "\nAnorthosite: " + numAnorthosite
            + "\nMare Basalt: " + numMareBasalt
            + "\nParadot: " + numParadot;
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
}
