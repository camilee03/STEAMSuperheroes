using TMPro;
using UnityEngine;

public class ChangeName : MonoBehaviour
{
    [SerializeField] TMP_InputField prevName;

    private void Start()
    {
        if (Globals.Instance.name != prevName.text) { prevName.text = Globals.Instance.name; }
    }

    public void UpdateSuperheroName(string name)
    {
        Globals.Instance.name = name;
    }
}
