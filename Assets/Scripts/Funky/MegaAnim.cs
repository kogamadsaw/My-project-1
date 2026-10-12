using UnityEngine;
using TMPro;

public class MegaAnim : MonoBehaviour
{
    private TMP_Text buttonText;

    [Header("Настройки мигания")]
    public float blinkSpeed = 3.0f;
    public float minAlpha = 0.2f;
    public float maxAlpha = 1.0f;

    void Start()
    {
        buttonText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (buttonText != null)
        {
            float progress = Mathf.PingPong(Time.time * blinkSpeed, 1.0f);

            float currentAlpha = Mathf.Lerp(minAlpha, maxAlpha, progress);

            Color newColor = buttonText.color;
            newColor.a = currentAlpha;
            buttonText.color = newColor;
        }
    }
}
