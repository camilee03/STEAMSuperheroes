using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CodeSelectSystem : MonoBehaviour
{
    //Selection system for code blocks

    ProgrammingMinigameManager gameManager = null;
    [SerializeField] GameObject blockCreationOrigin;
    [SerializeField] float addSpaceMargin = 10;
    Vector2 originPos;

    [Header("BlockParent")]
    [SerializeField] Transform blockParentTransform = null;

    [Header("DEBUG")]
    [SerializeField] GameObject selectedObject = null;
    [SerializeField] Color savedColor;
    [SerializeField] public List<GameObject> codeBlocks = new List<GameObject>(); //should always have loop in it

    private void Start()
    {
        originPos = blockCreationOrigin.transform.position;
        gameManager = FindFirstObjectByType<ProgrammingMinigameManager>();
    }
    //Block Selected
    public void SelectBlock(GameObject obj)
    {
        if (gameManager.GetPlayStatus()) return;
        if(selectedObject != null && obj != selectedObject)
        {
            DeselectBlockUnhighlight();
            SelectBlockHighlight(obj);
        }
        else if (obj == selectedObject)
        {
            DeselectBlockUnhighlight();
        } else
        {
            SelectBlockHighlight(obj);
        }
    }
    //Highlight selected block
    void SelectBlockHighlight(GameObject obj)
    {
        if(obj) Debug.Log("Highlight new block: " + obj.name);
        selectedObject = obj;
        savedColor = obj.GetComponent<Image>().color;
        obj.GetComponent<Image>().color = Color.yellow;
    }
    //Unlighlight previously selected block
    public void DeselectBlockUnhighlight()
    {
        if(selectedObject) Debug.Log("Unhiglight block: " + selectedObject.name);

        if (selectedObject) {
            selectedObject.GetComponent<Image>().color = savedColor;
            selectedObject = null;
        }
    }
    //Add a new code block
    public void AddCodeBlockAfterSelected(GameObject newBlock)
    {
        if (!gameManager.CodeEditable) return;
        if (!selectedObject)
        {
            Destroy(newBlock);
            return;
        }

        int originIdx = codeBlocks.IndexOf(selectedObject);
        codeBlocks.Insert(originIdx + 1, newBlock);
        Debug.Log("Added Gameobject: " + newBlock + " to codeBlocks");
        DeselectBlockUnhighlight();
        SelectBlockHighlight(newBlock);

        newBlock.transform.SetParent(blockParentTransform.transform);
        newBlock.transform.SetSiblingIndex(originIdx + 1);
    }
    //Remove selected block
    public void RemoveSelectedBlock() //Called By Button
    {
        if (!gameManager.CodeEditable) return;
        if (!selectedObject) return;
        Debug.Log("Attempting to remove block: " + selectedObject.name);
        if (selectedObject.GetComponent<CodeBlock>().GetCanBeTrashed())
        {
            codeBlocks.Remove(selectedObject);
            Debug.Log("Removed Gameobject: " + selectedObject + " from codeBlocks");
            Destroy(selectedObject);
            selectedObject = null;
        }
    }
    //Remove all blocks from the list
    public void RemoveAllBlocks() //Called By Button
    {
        Debug.Log("Attempting to clear blocks");
        GameObject start = codeBlocks[0];
        for (int i = 1; i < codeBlocks.Count; i++)
        {
            Destroy(codeBlocks[i]);
        }
        codeBlocks.Clear();
        codeBlocks.Add(start);
        selectedObject = null;
    }
    //Getter for code block list
    public List<GameObject> GetCodeBlocksList()
    {
        return codeBlocks;
    }
}
