using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue Slide")]
public class Dialogue_SO : ScriptableObject {
    public string text { get; }
    public bool type { get; }
}
