using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LogicGate : MonoBehaviour
{
    //Logic gate logic, has all possible logic conditions and can change within the same object

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
    [SerializeField] bool showVisualText = true;
    [SerializeField] TextMeshProUGUI visualText = null;
    [Header("Gate Choice Menu - For Changable Only")]
    [SerializeField] bool andAllowed = true;
    [SerializeField] bool orAllowed = true;
    [SerializeField] bool notAllowed = true;
    [Header("Input/Output Gates")]
    [SerializeField] LogicGate[] inputGates;
    [SerializeField] LogicGate[] outputGates;
    [Header("DEBUG")]
    [SerializeField] protected LOGIC_STATE gateState = LOGIC_STATE.EMPTY;
    [SerializeField] protected int gateValue = 2; //instead int wehre 0 = false, 1 = true, 2 = null value (empty gate)

    //Init
    private void Start()
    {
        circuitsDropdown = FindFirstObjectByType<CircuitsDropdown>();
        circuitManager = FindFirstObjectByType<CircuitsManager>();
        highlight.SetActive(false);
        //if (gateState == LOGIC_STATE.START) gateValue = true;
        if (!showVisualText) visualText.text = "";
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
    //Change the visual of a logic gate when changed
    void UpdateVisual()
    {
        DisableAllVisuals();
        switch (gateState)
        {
            case LOGIC_STATE.START:
                gateVisuals[0].SetActive(true);
                if (showVisualText) visualText.text = "";
                break;
            case LOGIC_STATE.END:
                gateVisuals[1].SetActive(true);
                if (showVisualText) visualText.text = "";
                break;
            case LOGIC_STATE.EMPTY:
                gateVisuals[2].SetActive(true);
                if (showVisualText) visualText.text = "";
                break;
            case LOGIC_STATE.AND:
                gateVisuals[3].SetActive(true);
                if (showVisualText) visualText.text = "AND";
                break;
            case LOGIC_STATE.OR:
                gateVisuals[4].SetActive(true);
                if (showVisualText) visualText.text = "OR";
                break;
            case LOGIC_STATE.NOT:
                gateVisuals[5].SetActive(true);
                if (showVisualText) visualText.text = "NOT";
                break;
        }

    }
    //Turn off all gate visuals
    void DisableAllVisuals() {
        for (int i = 0; i < gateVisuals.Length; i++) {
            gateVisuals[i].SetActive(false);
        }
    }
    //Called by self or other gate
    public void UpdateLogic() 
    {
        int newValue = 2;
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
    //Start Logic Gate
    int Logic_START()
    {
        return gateValue;
    }
    //Unofficial rule - This gate can only take 1 input
    //End Logic Gate
    int Logic_END() 
    {
        int inputValue = inputGates[0].GetGateValue();
        if (inputValue == 1)
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
                gateVisuals[1].GetComponentInChildren<SpriteRenderer>().color = Color.red;
            }
        }
        return inputValue;
    }
    //Empty Logic Gate
    int Logic_EMPTY()
    {
        return 2;
    }
    //And Logic Gate
    int Logic_AND()
    {
        int temporaryState = 1;
        if (inputGates.Length < 2) return 2; //can't have less than 2 inputs.
        for (int i = 0; i < inputGates.Length; i++)
        {
            if (inputGates[i].GetGateValue() == 0)
            {
                temporaryState = 0;
                break;
            }
        }
        return temporaryState;
    }
    //Or Logic Gate
    int Logic_OR()
    {
        int temporaryState = 0;
        if (inputGates.Length < 2) return 2; //can't have less than 2 inputs.
        for (int i = 0; i < inputGates.Length; i++)
        {
            if (inputGates[i].GetGateValue() == 1)
            {
                temporaryState = 1;
                break;
            }
        }
        return temporaryState;
    }
    //Not Logic Gate
    int Logic_NOT()//can only take 1 input
    {
        if(inputGates[0].GetGateValue() == 0) {
            return 1;
        }else if (inputGates[0].GetGateValue() == 1) {
            return 0;
        } else {
            return 2;
        }
    }
    #endregion
    //Getter for gate value
    public int GetGateValue()
    {
        return gateValue;
    }
}
