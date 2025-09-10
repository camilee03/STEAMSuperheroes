using UnityEngine;

[CreateAssetMenu(fileName = "Example", menuName = "Scriptable Objects/Example")]
public class Example : ScriptableObject
{
    [Range(0.0f, 10.0f)]
    public float health;

    [SerializeField] GameObject player;

    [SerializeField] string value = "Temp";
}
