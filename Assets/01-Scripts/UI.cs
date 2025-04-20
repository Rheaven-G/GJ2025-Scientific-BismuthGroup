using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Slider progressBar;
    public Image fillImage; // assign this in Inspector
    public int score = 0;

    private Color startColor = Color.blue;
    private Color endColor = new Color(1f, 0.5f, 0f); // orange (R=1, G=0.5, B=0)

    private void Update()
    {
        Debug.Log("" + progressBar.value);

        progressBar.value = score;

        // Gradually change color based on score (0 to 100)
        float t = Mathf.InverseLerp(0, 100, score); // converts score to 0–1 range
        fillImage.color = Color.Lerp(startColor, endColor, t);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (score <= 100)
            {
                score++;
            }
        }
    }
}
