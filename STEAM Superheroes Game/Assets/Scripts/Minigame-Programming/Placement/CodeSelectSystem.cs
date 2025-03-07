using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CodeSelectSystem : MonoBehaviour
{
    ProgrammingMinigameManager gameManager = null;
    [SerializeField] GameObject blockCreationOrigin;
    [SerializeField] float addSpaceMargin = 10;
    Vector2 originPos;

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
        if(selectedObject != null && selectedObject != obj)
        {
            //UNHIGHLIGHT Previous One 
            selectedObject.GetComponent<Image>().color = savedColor;
            selectedObject = null;
        }
        
        if (obj == selectedObject)
        {
            //UNHIGHLIGHT IT
            obj.GetComponent<Image>().color = savedColor;
            selectedObject = null;
        } else
        {
            selectedObject = obj;
            savedColor = obj.GetComponent<Image>().color;
            obj.GetComponent<Image>().color = Color.yellow;
        }
    }
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
        SelectBlock(newBlock);
        UpdateVisual();
    }
    public void RemoveSelectedBlock() //Called By Button
    {
        if (!gameManager.CodeEditable) return;
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
    public void RemoveAllBlocks() //Called By Button
    {
        selectedObject = null;
        GameObject start = codeBlocks[0];
        for (int i = 1; i < codeBlocks.Count; i++)
        {
            Destroy(codeBlocks[i]);
        }
        codeBlocks.Clear();
        codeBlocks.Add(start);
        selectedObject = null;
        UpdateVisual();
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
