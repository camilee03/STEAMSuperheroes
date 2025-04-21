using UnityEngine;

public class StartPoint : LogicGate
{
    //Subclass of LogicGate for start point

    [Header("Start Point")]
    [SerializeField] bool canBeToggled = true;
    [SerializeField] GameObject lightVisual = null;

    //Currently disabled
    public void Toggle() //Called by button for Start Points
    {
        return;

        //if (canBeToggled)
        //{
        //    gateValue = !gateValue;
        //    lightVisual.SetActive(!lightVisual.activeSelf);
        //    CascadeUpdateLogic();
        //}
    }
}
