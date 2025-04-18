using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    //Handles dialogue between characters

    [Header("Setup")]
    [SerializeField] Dialogue_SO[] dialogueOrder = null;
    int dialogueIndex = 0;

    [Header("UI")]
    [SerializeField] GameObject dialogueCanvas = null;
    [SerializeField] GameObject leftSide = null;
    [SerializeField] GameObject rightSide = null;
    [SerializeField] TextMeshProUGUI leftSideText = null;
    [SerializeField] TextMeshProUGUI rightSideText = null;
    [SerializeField] Image leftSideImageSource = null;
    [SerializeField] Image rightSideImageSource = null;
    [SerializeField] Image extraImageSource = null;
    [SerializeField] Image leftSideImageUnhighlight = null;
    [SerializeField] Image rightSideImageUnhighlight = null;
    [SerializeField] Image leftSideTextUnhighlight = null;
    [SerializeField] Image rightSideTextUnhighlight = null;

    [Header("DEBUG")]
    [SerializeField] Sprite defaultPlayerImage;

    //Setup the Dialogue Canvas
    private void Start() {
        leftSideText.text = null;
        rightSideText.text = null;
        leftSide.SetActive(false);
        rightSide.SetActive(false);
        LoadDialoguePart();
    }
    //Check if user left clicks to move to next dialogue sequence
    private void Update() {
        if (dialogueCanvas.activeSelf) {
            if (Input.GetMouseButtonDown(0)) {
                LoadDialoguePart();
            }
        }
    }
    //Loads the text of the current dialogueIndex
    void LoadDialoguePart() {
        if(dialogueIndex > dialogueOrder.Length - 1) { //out of dialogue options
            CloseDialogue();
            return;
        }

        Dialogue_SO currentDialogue = dialogueOrder[dialogueIndex];
        if (currentDialogue.OnLeftSide) { //left side dialogue
            LoadSide(currentDialogue, leftSide, leftSideImageSource, leftSideImageUnhighlight, leftSideText);

            //toggle gray highlight
            leftSideImageUnhighlight.enabled = false;
            rightSideImageUnhighlight.enabled = true;
            leftSideTextUnhighlight.enabled = false;
            rightSideTextUnhighlight.enabled = true;
        } else { //right side dialogue
            LoadSide(currentDialogue, rightSide, rightSideImageSource, rightSideImageUnhighlight, rightSideText);

            //toggle gray highlight
            leftSideImageUnhighlight.enabled = true;
            rightSideImageUnhighlight.enabled = false;
            leftSideTextUnhighlight.enabled = true;
            rightSideTextUnhighlight.enabled = false;
        }

        dialogueIndex++;
    }
    //Load dialogue and image into the given side
    void LoadSide(Dialogue_SO dialogueSO, GameObject side, Image sideImageSource, Image sideImageHighlight, TextMeshProUGUI sideText) {
        if (!side.activeSelf) side.SetActive(true);

        if (dialogueSO.UsesPlayerImage) {
            /*
             * TEMP - CHANGE THIS LATER TO USE THE PLAYER'S CUSTOMIZED CHARACTER
             */
            sideImageSource.sprite = defaultPlayerImage;
            sideImageHighlight.sprite = defaultPlayerImage;
        } else {
            sideImageSource.sprite = dialogueSO.SpeakerImage;
            sideImageHighlight.sprite = dialogueSO.SpeakerImage;
        }

        sideText.text = dialogueSO.DialogueText;

        if (dialogueSO.ExtraImage) {
            extraImageSource.gameObject.SetActive(true);
            extraImageSource.sprite = dialogueSO.ExtraImage;
        } else {
            extraImageSource.gameObject.SetActive(false);
        }
    }
    //Turn off dialogue canvas
    void CloseDialogue() {
        dialogueCanvas.SetActive(false);
    }
}
