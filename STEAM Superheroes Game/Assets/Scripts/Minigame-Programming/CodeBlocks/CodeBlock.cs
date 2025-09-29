using UnityEngine;
using UnityEngine.UI;

public class CodeBlock : MonoBehaviour
{
    [SerializeField] ProgrammingMinigameManager.CODE_COMMAND command;
    [SerializeField] bool canBeTrashed = false;
    CodeSelectSystem sys = null;

    [SerializeField] bool isStartBlock = false;
    private void Start()
    {
        sys = FindFirstObjectByType<CodeSelectSystem>();
    }
    public ProgrammingMinigameManager.CODE_COMMAND GetCommand()
    {
        return command;
    }
    public void SelectThis() //Called By Button
    {
        sys.SelectBlock(gameObject);
    }
    public bool GetCanBeTrashed()
    {
        return canBeTrashed;
    }
    public void ActiveColor(bool onOff)
    {
        if (!isStartBlock)
        {
            if (onOff)
            {
                gameObject.GetComponent<Image>().color = Color.cyan;
            } else
            {
                gameObject.GetComponent<Image>().color = Color.white;
            }
        }
        
    }
}
