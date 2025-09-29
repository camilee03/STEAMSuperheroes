using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] float maxVal = 10f;
    [SerializeField] float minVal = 5f;
    [SerializeField] float zRot = .1f;
    private void Start()
    {
        zRot = Random.Range(minVal, maxVal) * (Random.Range(0, 2) == 0 ? -1 : 1);
    }
    void Update()
    {
        transform.Rotate(0, 0, zRot * Time.deltaTime, Space.Self);
    }
}
