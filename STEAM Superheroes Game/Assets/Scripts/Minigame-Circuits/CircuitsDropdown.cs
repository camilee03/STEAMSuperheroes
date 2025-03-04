using TMPro;
using UnityEngine;

public class CircuitsDropdown : MonoBehaviour
{
    //Dropdowns
    [SerializeField] GameObject dropdownCanvas = null;
    [SerializeField] TMP_Dropdown dropdown = null;
    [Header("DEBUG")]
    [SerializeField] LogicGate currentGate = null;
    
    public void OnDropdownSelect()
    {
        int pickedIdx = dropdown.value;
        string selectedStr = dropdown.options[pickedIdx].ToString();
        Debug.Log("Dropdown chose: " + selectedStr);

        if(currentGate)currentGate.ChangeGate(pickedIdx);
        CloseDropdownMenu();
    }
    public void ToggleDropdown(LogicGate gate)
    {
        if (dropdownCanvas.activeSelf)
        {
            Debug.Log("toggle - Turn off dropdown");

            if (gate != currentGate && gate != null)
            {
                currentGate = gate;
                dropdown.value = 0;
            } else
            {
                //close dropdown
                CloseDropdownMenu();
            }
        } else
        {
            Debug.Log("toggle - Turn on dropdown");

            //turn it on
            OpenDropdownMenu();
            currentGate = gate;
        }
    }
    void OpenDropdownMenu()
    {
        Debug.Log("Turn on dropdown");

        dropdownCanvas.SetActive(true);
        dropdown.value = 0;
    }
    void CloseDropdownMenu()
    {
        Debug.Log("Turn off dropdown");

        dropdownCanvas.SetActive(false);
        currentGate = null;
    }
}
