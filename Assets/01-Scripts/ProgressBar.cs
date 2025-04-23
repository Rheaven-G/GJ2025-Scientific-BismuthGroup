using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider progressBar;
    public Image fillImage;
    public float score = 0;

    private Color startColor = Color.blue;
    private Color endColor = Color.red;

    void Start()
    {

    }

    private void Update()
    {
        // Update bar value and color
        progressBar.value = score;
        float t = Mathf.InverseLerp(0, 100, score);
        fillImage.color = Color.Lerp(startColor, endColor, t);
    }
}
