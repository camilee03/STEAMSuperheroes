using UnityEngine;

public class CodeBlock : MonoBehaviour
{
    [SerializeField] ProgrammingMinigameManager.CODE_COMMAND command;
    [SerializeField] bool canBeTrashed = false;
    CodeSelectSystem sys = null;
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
}
