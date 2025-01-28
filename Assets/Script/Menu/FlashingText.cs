using UnityEngine;
using TMPro;

public class FlashingText : MonoBehaviour
{
    public TMP_Text textComponent;
    public float pulseSpeed = 1f;
    public float maxScale = 1.2f;
    public float minScale = 0.8f;

    private Vector3 initialScale;

    void Start()
    {
        if (textComponent == null)
        {
            textComponent = GetComponent<TMP_Text>();
        }
        initialScale = textComponent.transform.localScale;
    }

    void Update()
    {
        float scale = Mathf.Lerp(minScale, maxScale, Mathf.PingPong(Time.time * pulseSpeed, 1));
        textComponent.transform.localScale = initialScale * scale;
    }
}
