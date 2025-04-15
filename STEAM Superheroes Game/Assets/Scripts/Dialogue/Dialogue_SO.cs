using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue Slide")]
public class Dialogue_SO : ScriptableObject {
    [Header("Properties")]
    [SerializeField] bool onLeftSide = false; //0 is left, 1 is right
    [SerializeField] bool usesPlayerImage = false;
    [SerializeField] Sprite speakerImage = null; //not used if usesPlayerImage is true
    [SerializeField] Sprite extraImage = null;
    [TextArea(15, 20)]
    [SerializeField] string dialogueText = "";

    public bool OnLeftSide => onLeftSide;
    public string DialogueText => dialogueText;
    public bool UsesPlayerImage => usesPlayerImage;
    public Sprite SpeakerImage => speakerImage;
    public Sprite ExtraImage => extraImage;
}
