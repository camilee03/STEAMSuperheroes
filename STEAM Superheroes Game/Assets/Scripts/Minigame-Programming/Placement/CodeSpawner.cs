using UnityEngine;

public class CodeSpawner : MonoBehaviour
{
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
    public void Spawn() //Called By Button
    {
        GameObject cb = Instantiate(codeBlock, parentCanvas.transform);
        if (gameManager.CodeEditable) {
            sys.AddCodeBlockAfterSelected(cb);
        }
    }
}
