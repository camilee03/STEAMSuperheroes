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

    [Header("Gate Visuals")]
    [SerializeField] GameObject[] gateVisuals; //0 = START, 1 = END, 2 = EMPTY, 3 = AND, 4 = OR, 5 = NOT
    [SerializeField] protected LOGIC_STATE gateState = LOGIC_STATE.EMPTY;
    [SerializeField] bool canBeChanged = true;
    [Header("DEBUG")]
    [SerializeField] protected bool value = true;

    LogicGate[] inputGates;
    LogicGate[] outputGates;

    
    public void OpenDropdown() //Called by clicking on itself (as a button)
    {
        //enable dropdown ui
    }
    public void CloseDropdown()
    {
        //close dropdown ui
    }
    public void ChangeGate(LOGIC_STATE state) //Called by button for logic gate
    {
        if (!canBeChanged) return;
        gateState = state;
        CloseDropdown();
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
    public void UpdateLogic() //Called by self or other gate
    {
        //potentially changes the output here. then, if it does, call the same method in the output. should send a cascade.
        bool preStateSave = value;
        bool newValue = value;
        switch (gateState)
        {
            case LOGIC_STATE.START:  //this should never be called
                newValue = Logic_START();
                break;
            case LOGIC_STATE.END:
                newValue = Logic_END();
                break;
            case LOGIC_STATE.EMPTY:  //this should never be called
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
        //when call funciton, return to newvalue
        if(newValue != preStateSave)
        {
            for (int i = 0; i < outputGates.Length; i++)
            {
                outputGates[i].UpdateLogic();
            }
        }
    }
    #region GateLogic
    bool Logic_START()
    {
        return value;
    }
    bool Logic_END() //can only take 1 input
    {
        bool save = inputGates[0].value;
        if (save)
        {
            //turn on light
            //end the game as win
        }
        return save;
    }
    bool Logic_EMPTY()
    {
        return value;
    }
    bool Logic_AND()
    {
        bool temporaryState = true;
        for (int i = 0; i < inputGates.Length; i++)
        {
            if (!inputGates[i].value)
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
            if (inputGates[i].value)
            {
                temporaryState = true;
                break;
            }
        }
        return temporaryState;
    }
    bool Logic_NOT()//can only take 1 input
    {
        return !inputGates[0].value;
    }
    #endregion
    void DisableAllVisuals()
    {
        for(int i = 0; i < gateVisuals.Length; i++)
        {
            gateVisuals[i].SetActive(false);
        }
    }
    ////If normal version is too long/doesnt work, try this method (1 subclass per gate):
    //protected virtual void ExecuteLogic() //Overridden in subclasses
    //{
    //    return;
    //}

    public bool GetValue()
    {
        return value;
    }



}
