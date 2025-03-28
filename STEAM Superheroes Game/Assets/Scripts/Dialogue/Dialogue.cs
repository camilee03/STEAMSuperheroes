using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField] Dialogue_SO startingPlayerDialogue;
    [SerializeField] Dialogue_SO startingNonPlayerDialogue;
    [SerializeField] Dialogue_SO[] dialogueOrder = null; //both player and game text goes in this.
    int dialogueIndex = 0;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI playerText = null;
    [SerializeField] TextMeshProUGUI nonPlayerText = null;
    [SerializeField] Image playerImage = null;
    [SerializeField] Image nonPlayerImage = null;

    private void Start() {
        playerText.text = startingPlayerDialogue.text;
        nonPlayerText.text = startingNonPlayerDialogue.text;
    }
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            NextText();
        }
    }
    public void NextText() {
        if(dialogueIndex > dialogueOrder.Length - 1) { //out of dialogue options
            CloseDialogue();
        }

        Dialogue_SO  dialogue = dialogueOrder[dialogueIndex];
        if (dialogue.type) { //player text
            playerText.text = dialogue.text;
            HighlightImage(playerImage);
        } else { //non player text
            nonPlayerText.text = dialogue.text;
            HighlightImage(nonPlayerImage);
        }
        dialogueIndex++;
    }

    void HighlightImage(Image image) {
        DeHighlightImage(playerImage);
        DeHighlightImage(nonPlayerImage);

        //hihglight image here
    }
    void DeHighlightImage(Image image) {
        //dehighlight image here
    }
    void CloseDialogue() {
        gameObject.SetActive(false);
    }
}
