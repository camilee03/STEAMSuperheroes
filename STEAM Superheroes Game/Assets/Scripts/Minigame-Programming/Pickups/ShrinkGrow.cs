using UnityEngine;

public class ShrinkGrow : MonoBehaviour
{
    public float speed = 2f;
    public float minScale = 0.5f;
    public float maxScale = 1.5f;

    void Update()
    {
        float scale = Mathf.Lerp(minScale, maxScale, Mathf.PingPong(Time.time * speed, 1));
        transform.localScale = new Vector3(scale, scale, 1f);
    }
}
