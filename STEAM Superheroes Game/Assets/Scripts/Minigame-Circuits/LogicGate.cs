using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LogicGate : MonoBehaviour
{
    public enum LOGIC_STATE
    {
        START,
        END,
        EMPTY,
        AND,
        OR,
        NOT,
    }//To Add: XOR, etc.

    [Header("Managing")]
    [SerializeField] string gateName = "";
    CircuitsManager circuitManager = null;
    bool endGateCheck = false;
    CircuitsDropdown circuitsDropdown = null;
    [Header("Gate Visuals")]
    [SerializeField] GameObject highlight = null;
    [SerializeField] GameObject[] gateVisuals; //0 = START, 1 = END, 2 = EMPTY, 3 = AND, 4 = OR, 5 = NOT
    [SerializeField] bool canBeChanged = true;
    [Header("Gate Choice Menu - For Changable Only")]
    [SerializeField] bool andAllowed = true;
    [SerializeField] bool orAllowed = true;
    [SerializeField] bool notAllowed = true;
    [Header("Input/Output Gates")]
    [SerializeField] LogicGate[] inputGates;
    [SerializeField] LogicGate[] outputGates;
    [Header("DEBUG")]
    [SerializeField] protected LOGIC_STATE gateState = LOGIC_STATE.EMPTY;
    [SerializeField] protected bool gateValue = false;


    private void Start()
    {
        circuitsDropdown = FindFirstObjectByType<CircuitsDropdown>();
        circuitManager = FindFirstObjectByType<CircuitsManager>();
        highlight.SetActive(false);
        if (gateState == LOGIC_STATE.START) gateValue = true;
    }
    //Called by clicking on itself (as a button)
    public void ToggleDropdown() 
    {
        if(canBeChanged) circuitsDropdown.OpenChoiceMenu(this, andAllowed, orAllowed, notAllowed);
    }
    //Called by CircuitsManager
    public void Highlight() {
        highlight.SetActive(true);
    }
    //Called by CircuitsManager
    public void DeHighlight() {
        highlight.SetActive(false);
    }
    //Called by CircuitsManager
    public void ChangeGate(int choice) 
    {
        if (!canBeChanged) return;

        switch (choice)
        {
            case 0:
                gateState = LOGIC_STATE.EMPTY;
                Debug.Log("Changing gate into EMPTY");
                break;
            case 1:
                gateState = LOGIC_STATE.AND;
                Debug.Log("Changing gate into AND");
                break;
            case 2:
                gateState = LOGIC_STATE.OR;
                Debug.Log("Changing gate into OR");
                break;
            case 3:
                gateState = LOGIC_STATE.NOT;
                Debug.Log("Changing gate into NOT");
                break;
        }
        UpdateVisual();
        UpdateLogic();
    }
    void UpdateVisual()
    {
        DisableAllVisuals();
        switch (gateState)
        {
            case LOGIC_STATE.START:
                gateVisuals[0].SetActive(true);
                break;
            case LOGIC_STATE.END:
                gateVisuals[1].SetActive(true);
                break;
            case LOGIC_STATE.EMPTY:
                gateVisuals[2].SetActive(true);
                break;
            case LOGIC_STATE.AND:
                gateVisuals[3].SetActive(true);
                break;
            case LOGIC_STATE.OR:
                gateVisuals[4].SetActive(true);
                break;
            case LOGIC_STATE.NOT:
                gateVisuals[5].SetActive(true);
                break;
        }

    }
    void DisableAllVisuals() {
        for (int i = 0; i < gateVisuals.Length; i++) {
            gateVisuals[i].SetActive(false);
        }
    }
    //Called by self or other gate
    public void UpdateLogic() 
    {
        bool newValue = false;
        switch (gateState)
        {
            case LOGIC_STATE.START: 
                newValue = Logic_START();
                break;
            case LOGIC_STATE.END:
                newValue = Logic_END();
                break;
            case LOGIC_STATE.EMPTY:
                newValue = Logic_EMPTY();
                break;
            case LOGIC_STATE.AND:
                newValue = Logic_AND();
                break;
            case LOGIC_STATE.OR:
                newValue = Logic_OR();
                break;
            case LOGIC_STATE.NOT:
                newValue = Logic_NOT();
                break;
        }
        if(newValue != gateValue) {
            gateValue = newValue;

            CascadeUpdateLogic();
        }
    }
    //Called from this script or StartPoint script
    protected void CascadeUpdateLogic() {
        for (int i = 0; i < outputGates.Length; i++) {
            outputGates[i].UpdateLogic();
        }
    }
    #region GateLogic
    bool Logic_START()
    {
        return gateValue;
    }
    //Unofficial rule - This gate can only take 1 input
    bool Logic_END() 
    {
        bool inputValue = inputGates[0].GetGateValue();
        if (inputValue)
        {
            if (!endGateCheck) {
                Debug.Log("adding success");
                endGateCheck = true;
                circuitManager.AddSuccess();
                gateVisuals[1].GetComponentInChildren<SpriteRenderer>().color = Color.green;
            }
        } else
        {
            if (endGateCheck) {
                Debug.Log("removing success");
                endGateCheck = false;
                circuitManager.RemoveSuccess();
                gateVisuals[1].GetComponentInChildren<SpriteRenderer>().color = Color.black;
            }
        }
        return inputValue;
    }
    bool Logic_EMPTY()
    {
        return false;
    }
    bool Logic_AND()
    {
        bool temporaryState = true;
        for (int i = 0; i < inputGates.Length; i++)
        {
            if (!inputGates[i].GetGateValue())
            {
                temporaryState = false;
                break;
            }
        }
        return temporaryState;
    }
    bool Logic_OR()
    {
        bool temporaryState = false;
        for (int i = 0; i < inputGates.Length; i++)
        {
            if (inputGates[i].GetGateValue())
            {
                temporaryState = true;
                break;
            }
        }
        return temporaryState;
    }
    bool Logic_NOT()//can only take 1 input
    {
        return !inputGates[0].GetGateValue();
    }
    #endregion
    
    public bool GetGateValue()
    {
        return gateValue;
    }
}
