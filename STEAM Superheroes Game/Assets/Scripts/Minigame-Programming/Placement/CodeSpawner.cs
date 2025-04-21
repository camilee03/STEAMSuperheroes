using UnityEngine;

public class CodeSpawner : MonoBehaviour
{
    //Creates code blocks

    //System
    ProgrammingMinigameManager gameManager = null;
    CodeSelectSystem sys = null;
    [SerializeField] GameObject parentCanvas = null;

    //Individual
    [SerializeField] GameObject codeBlock;

    //Called from Button
    private void Start()
    {
        gameManager = FindFirstObjectByType<ProgrammingMinigameManager>();
        sys = FindFirstObjectByType<CodeSelectSystem>();
    }
    //Spawn a new code block
    public void Spawn() //Called By Button
    {
        if (!gameManager.GetPlayStatus())
        {
            GameObject cb = Instantiate(codeBlock, parentCanvas.transform);
            if (gameManager.CodeEditable)
            {
                sys.AddCodeBlockAfterSelected(cb);
            }
        }
    }
}
