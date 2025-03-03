using UnityEngine;

public class StartPoint : LogicGate
{
    [SerializeField] GameObject lightVisual = null;
    public void Toggle() //Called by button for Start Points
    {
        value = !value;
        lightVisual.SetActive(!lightVisual.activeSelf);
    }
}
