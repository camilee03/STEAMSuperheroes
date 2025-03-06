using UnityEngine;

public class StartPoint : LogicGate
{
    [Header("Start Point")]
    [SerializeField] bool canBeToggled = true;
    [SerializeField] GameObject lightVisual = null;
    public void Toggle() //Called by button for Start Points
    {
        if (canBeToggled)
        {
            value = !value;
            lightVisual.SetActive(!lightVisual.activeSelf);
            UpdateLogic();
        }
    }
}
