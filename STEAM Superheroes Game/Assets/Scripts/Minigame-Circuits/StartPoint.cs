using UnityEngine;

public class StartPoint : LogicGate
{
    [Header("Start Point")]
    [SerializeField] bool canBeToggled = true;
    [SerializeField] GameObject lightVisual = null;
    public void Toggle() //Called by button for Start Points
    {
        return;

        if (canBeToggled)
        {
            gateValue = !gateValue;
            lightVisual.SetActive(!lightVisual.activeSelf);
            CascadeUpdateLogic();
        }
    }
}
