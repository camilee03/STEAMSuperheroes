using TMPro;
using UnityEngine;

public class CircuitsDropdown : MonoBehaviour
{
    //Choice bar for replacing logic gates

    [Header("Gate Choices")]
    [SerializeField] GameObject emptyButton = null;
    [SerializeField] GameObject andButton = null;
    [SerializeField] GameObject orButton = null;
    [SerializeField] GameObject notButton = null;
    [Header("DEBUG")]
    [SerializeField] LogicGate selectedGate = null;

    //Called by a LogicGate script
    public void OpenChoiceMenu(LogicGate gate, bool andCheck, bool orCheck, bool notCheck) {
        CloseChoiceMenu();
        
        if(selectedGate)selectedGate.DeHighlight();

        if (gate != selectedGate) {
            selectedGate = gate;
            selectedGate.Highlight();

            emptyButton.SetActive(true);
            if (andCheck) {
                andButton.SetActive(true);
            }
            if (orCheck) {
                orButton.SetActive(true);
            }
            if (notCheck) {
                notButton.SetActive(true);
            }
        } else {
            selectedGate = null;
        }
    }
    //Remove all choices from the menu
    void CloseChoiceMenu() {
        //Deactivate All
        andButton.SetActive(false);
        orButton.SetActive(false);
        notButton.SetActive(false);
        emptyButton.SetActive(false);
    }
    //Called by menu button
    //0 is Empty, 1 is And, 2 is Or, 3 is Not
    public void ChangeToGate(int choice) { 
        if (!selectedGate) return;
        selectedGate.ChangeGate(choice);
        
        CloseChoiceMenu();
        selectedGate.DeHighlight();
        selectedGate = null;
    }
}
