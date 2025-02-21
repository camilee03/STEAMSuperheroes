using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CodeSelectSystem : MonoBehaviour
{
    [SerializeField] GameObject blockCreationOrigin;
    [SerializeField] float addSpaceMargin = 10;
    Vector2 originPos;

    [Header("DEBUG")]
    [SerializeField] GameObject selectedObject = null;
    [SerializeField] public List<GameObject> codeBlocks = new List<GameObject>(); //should always have loop in it

    private void Start()
    {
        originPos = blockCreationOrigin.transform.position;
    }
    //Block Selected
    public void SelectBlock(GameObject obj)
    {
        if (obj == selectedObject)
        {
            //UNHIGHLIGHT IT
            selectedObject = null;
        } else
        {
            selectedObject = obj;
            //HIGHLIGHT IT
        }
    }
    public void AddCodeBlockAfterSelected(GameObject newBlock)
    {
        if (!selectedObject) return;
        int originIdx = codeBlocks.IndexOf(selectedObject);
        codeBlocks.Insert(originIdx + 1, newBlock);
        Debug.Log("Added Gameobject: " + newBlock + " to codeBlocks");
        UpdateVisual();
    }
    public void RemoveSelectedBlock() //Called By Button
    {
        if (!selectedObject) return;
        if (selectedObject.GetComponent<CodeBlock>().GetCanBeTrashed())
        {
            codeBlocks.Remove(selectedObject);
            Debug.Log("Removed Gameobject: " + selectedObject + " from codeBlocks");
            Destroy(selectedObject);
            selectedObject = null;
            UpdateVisual();
        }
    }
    void UpdateVisual()
    {
        GameObject header = codeBlocks[0];
        header.transform.position = originPos;
        if (codeBlocks.Count > 1)
        {
            for (int i = 1; i < codeBlocks.Count; i++)
            {
                Vector2 newPos = new Vector2(originPos.x, originPos.y - (i * addSpaceMargin));
                codeBlocks[i].transform.position = newPos;
            }
        }
    }
    public List<GameObject> GetCodeBlocksList()
    {
        return codeBlocks;
    }
}
